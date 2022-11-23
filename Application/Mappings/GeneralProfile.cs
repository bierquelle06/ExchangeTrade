using AutoMapper;

using Application.Features.BankBranch.Commands.CreateBankBranch;
using Application.Features.BankBranch.Queries.GetAllBankBranchs;

using Application.Features.Integrator.Commands.CreateIntegrator;
using Application.Features.Integrator.Queries.GetAllIntegrators;

using Application.Features.BankAccountType.Commands.CreateBankAccountType;
using Application.Features.BankAccountType.Queries.GetAllBankAccountTypes;

using Application.Features.Currency.Commands.CreateCurrency;
using Application.Features.Currency.Queries.GetAllCurrency;

using Application.Features.Bank.Commands.CreateBank;
using Application.Features.Bank.Queries.GetAllBanks;

using Application.Features.BankAccount.Commands.CreateBankAccount;
using Application.Features.BankAccount.Queries.GetAllBankAccounts;

using Application.Features.BankAccountActivity.Commands.CreateBankAccountActivity;
using Application.Features.BankAccountActivity.Queries.GetAllBankAccountActivities;

using Application.Features.CurrencyActivity.Commands.CreateCurrencyActivity;
using Application.Features.CurrencyActivity.Queries.GetAllCurrencyActivities;

namespace Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            //Integrator
            CreateMap<Domain.Entities.Integrator.Integrator, GetAllIntegratorsViewModel>().ReverseMap();
            CreateMap<CreateIntegratorCommand, Domain.Entities.Integrator.Integrator>();
            CreateMap<GetAllIntegratorsQuery, GetAllIntegratorsParameter>();

            //Bank Branch
            CreateMap<Domain.Entities.BankBranch.BankBranch, GetAllBankBranchsViewModel>().ReverseMap();
            CreateMap<CreateBankBranchCommand, Domain.Entities.BankBranch.BankBranch>();
            CreateMap<GetAllBankBranchsQuery, GetAllBankBranchsParameter>();

            //BankAccountType
            CreateMap<Domain.Entities.BankAccountType.BankAccountType, GetAllBankAccountTypesViewModel>().ReverseMap();
            CreateMap<CreateBankAccountTypeCommand, Domain.Entities.BankAccountType.BankAccountType>();
            CreateMap<GetAllBankAccountTypesQuery, GetAllBankAccountTypesParameter>();

            //Currency
            CreateMap<Domain.Entities.Currency.Currency, GetAllCurrencyViewModel>().ReverseMap();
            CreateMap<CreateCurrencyCommand, Domain.Entities.Currency.Currency>();
            CreateMap<GetAllCurrencyQuery, GetAllCurrencyParameter>();

            //Bank
            CreateMap<Domain.Entities.Bank.Bank, GetAllBanksViewModel>().ReverseMap();
            CreateMap<CreateBankCommand, Domain.Entities.Bank.Bank>();
            CreateMap<GetAllBanksQuery, GetAllBanksParameter>();

            //Bank Account
            CreateMap<Domain.Entities.BankAccount.BankAccount, GetAllBankAccountsViewModel>().ReverseMap();
            CreateMap<CreateBankAccountCommand, Domain.Entities.BankAccount.BankAccount>();
            CreateMap<GetAllBankAccountsQuery, GetAllBankAccountsParameter>();

            //Bank Account Activity
            CreateMap<Domain.Entities.BankAccountActivity.BankAccountActivity, GetAllBankAccountActivitiesViewModel>().ReverseMap();
            CreateMap<CreateBankAccountActivityCommand, Domain.Entities.BankAccountActivity.BankAccountActivity>();
            CreateMap<GetAllBankAccountActivitiesQuery, GetAllBankAccountActivitiesParameter>();

            //Currency Activity
            CreateMap<Domain.Entities.CurrencyActivity.CurrencyActivity, GetAllCurrencyActivitiesViewModel>().ReverseMap();
            CreateMap<CreateCurrencyActivityCommand, Domain.Entities.CurrencyActivity.CurrencyActivity>();
            CreateMap<GetAllCurrencyActivitiesQuery, GetAllCurrencyActivitiesParameter>();
        }
    }
}
