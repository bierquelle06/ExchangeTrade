using Application.Configuration.Commands;
using Application.DTOs.InternalCommand;
using Application.Exceptions;
using Application.Interfaces.Repositories;
using Application.Wrappers;

using AutoMapper;
using Domain.Entities.BankAccount;
using Domain.Entities.BankAccountActivity;
using Domain.Entities.BankAccountActivityLog;
using Domain.Entities.Currency;
using Domain.Entities.Integrator;
using Domain.Settings;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Processing.InternalCommands
{
    /// <summary>
    /// ProcessInternalCommand
    /// </summary>
    public partial class ProcessInternalCommand : IRequest<Response<bool>>
    {
        public string Name { get; set; }
        public string Code { get; set; }
    }

    /// <summary>
    /// Process Internal Command Handler
    /// </summary>
    public class ProcessInternalCommandHandler : IRequestHandler<ProcessInternalCommand, Response<bool>>
    {
        public InternalCommandSettings _internalCommandSettings { get; }

        private readonly IIntegratorRepositoryAsync _integratorRepository;
        private readonly ICurrencyRepositoryAsync _currencyRepository;
        private readonly ICurrencyActivityRepositoryAsync _currencyActivityRepository;

        private readonly IBankRepositoryAsync _bankRepository;
        private readonly IBankAccountRepositoryAsync _bankAccountRepository;
        private readonly IBankAccountTypeRepositoryAsync _bankAccountTypeRepository;
        private readonly IBankAccountActivityRepositoryAsync _bankAccountActivityRepositoryAsync;
        private readonly IBankAccountActivityLogRepositoryAsync _bankAccountActivityLogRepositoryAsync;

        private readonly IMapper _mapper;

        private readonly ILogger<ProcessInternalCommandHandler> _logger;

        public ProcessInternalCommandHandler(
            IOptions<InternalCommandSettings> internalCommandSettings,
            IBankAccountRepositoryAsync bankAccountRepository,
            IBankAccountActivityRepositoryAsync bankAccountActivityRepository,
            IBankAccountActivityLogRepositoryAsync bankAccountActivityLogRepositoryAsync,
            IBankRepositoryAsync bankRepository,
            IIntegratorRepositoryAsync integratorRepository,
            ICurrencyRepositoryAsync currencyRepository,
            IBankAccountTypeRepositoryAsync bankAccountTypeRepository,
            ICurrencyActivityRepositoryAsync currencyActivityRepository,
            IMapper mapper,
            ILogger<ProcessInternalCommandHandler> logger)
        {
            this._internalCommandSettings = internalCommandSettings.Value;

            this._bankRepository = bankRepository;
            this._bankAccountRepository = bankAccountRepository;
            this._bankAccountActivityRepositoryAsync = bankAccountActivityRepository;
            this._bankAccountActivityLogRepositoryAsync = bankAccountActivityLogRepositoryAsync;
            this._bankAccountTypeRepository = bankAccountTypeRepository;

            this._integratorRepository = integratorRepository;
            this._currencyRepository = currencyRepository;
            this._currencyActivityRepository = currencyActivityRepository;

            this._mapper = mapper;

            this._logger = logger;
        }

        public async Task<Response<bool>> Handle(ProcessInternalCommand command, CancellationToken cancellationToken)
        {
            var start_hours = Int32.Parse(_internalCommandSettings.StartTime.Split(':')[0]);
            var start_minutes = Int32.Parse(_internalCommandSettings.StartTime.Split(':')[1]);

            var end_hours = Int32.Parse(_internalCommandSettings.EndTime.Split(':')[0]);
            var end_minutes = Int32.Parse(_internalCommandSettings.EndTime.Split(':')[1]);

            TimeSpan ts_Start = new TimeSpan(start_hours, start_minutes, 0);
            TimeSpan ts_End = new TimeSpan(end_hours, end_minutes, 0);
            TimeSpan now = DateTime.Now.TimeOfDay;

            try
            {
                if ((now > ts_Start) && (now < ts_End))
                {
                    //Tüm Currency Listesi (tek sefer)
                    var currencyList = await _currencyRepository.Find(p => p.IsDelete == false).ToListAsync();

                    //Currency Count
                    this._logger.LogInformation($"Currency List: {currencyList.Count}");

                    //Tüm Banka Hesaplarını çek.
                    var bankAccountList = await _bankAccountRepository.Find(p => p.IsDelete == false).ToListAsync();

                    //Bank Account Count
                    this._logger.LogInformation($"Bank Account List: {bankAccountList.Count}");

                    if (bankAccountList.Count > 0)
                    {
                        for (int i = 0; i < bankAccountList.Count; i++)
                        {
                            var bankAccount = bankAccountList[i];
                            var currencyBase = currencyList.Where(x => x.Id == bankAccount.CurrencyId).FirstOrDefault();

                            var integrator = await _integratorRepository.Find(x => x.Id == bankAccount.IntegratorId).SingleOrDefaultAsync();
                            if (integrator != null)
                            {
                                var startDate = DateTime.Now.AddDays(-7);
                                var endDate = DateTime.Now;

                                var executePluginResult = ExecuteProcessPlugin(bankAccount, currencyBase, currencyList, integrator, startDate, endDate);

                                var insertList = new List<Domain.Entities.CurrencyActivity.CurrencyActivity>();

                                for (int j = 0; j < executePluginResult.Count; j++)
                                {
                                    executePluginResult[j].CurrencyId = currencyList.Where(x => x.Code == executePluginResult[j].Symbol).First().Id;

                                    var item = executePluginResult[j];
                                    
                                    insertList.Add(new Domain.Entities.CurrencyActivity.CurrencyActivity()
                                    {
                                        CurrencyId = item.CurrencyId,
                                        Date = item.Date,
                                        Rate = decimal.Parse(item.Rate, new NumberFormatInfo() { NumberDecimalSeparator = "." }),
                                        SymbolBase = item.SymbolBase,
                                        Symbol = item.Symbol,
                                        TimeStamp = item.Timestamp
                                    });
                                }

                                var resultList = insertList.ToList();

                                if (resultList.Count > 0)
                                {
                                    for (int k = 0; k < resultList.Count; k++)
                                    {
                                        var findData = this._currencyActivityRepository.Find(x => x.Date == resultList[k].Date
                                        && x.TimeStamp == resultList[k].TimeStamp
                                        && x.SymbolBase == resultList[k].SymbolBase
                                        && x.Symbol == resultList[k].Symbol).Any();

                                        if(!findData)
                                            _ = await this._currencyActivityRepository.AddAsync(resultList[k]);
                                    }
                                }
                                else
                                    return new Response<bool>(false);
                            }
                        }

                        return new Response<bool>(true);
                    }
                    else
                        return new Response<bool>(false, "Failed! Account is not exist.");
                }
                else
                {
                    return new Response<bool>(false, "Failed! Timing : " + _internalCommandSettings.StartTime + " - " + _internalCommandSettings.EndTime);
                }
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"ProcessInternalCommandHandler::Failed! Error Message : {ex.Message}");

                return new Response<bool>(false, "Failed! Error Message : " + ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="bankAccount"></param>
        /// <param name="integrator"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        private List<InternalCommandCurrencyActivityRequest> ExecuteProcessPlugin(BankAccount bankAccount,
            Currency currencyBase,
            List<Currency> currencies,
            Integrator integrator,
            DateTime startDate,
            DateTime endDate)
        {
            List<InternalCommandCurrencyActivityRequest> currencyActivityRequests = new List<InternalCommandCurrencyActivityRequest>();

            try
            {
                if (integrator == null)
                    return null;

                if (integrator.IsDelete == true)
                    return null;

                switch (integrator.Code)
                {
                    case "EXCHANGEAPILAYER":

                        MeDirect.ExchangeRatesApi.ExchangeRatesApiPlugin exchangeRatesApiPlugin = new MeDirect.ExchangeRatesApi.ExchangeRatesApiPlugin();
                        currencyActivityRequests = exchangeRatesApiPlugin.UpdateCurrencyActivityTransaction(bankAccount, currencyBase, currencies, integrator, startDate, endDate);

                        break;

                    case "FIXERIO":


                        break;

                    default:
                        currencyActivityRequests = null;
                        break;
                }

                if (currencyActivityRequests.Count > 0)
                    this._logger.LogInformation($"ExecuteProcessPlugin : {currencyActivityRequests.Count}");
            }
            catch (Exception ex)
            {
                this._logger.LogError(ex, $"ExecuteProcessPlugin::Failed! Error Message : {ex.Message}");

                currencyActivityRequests = null;
            }

            return currencyActivityRequests;
        }
    }
}
