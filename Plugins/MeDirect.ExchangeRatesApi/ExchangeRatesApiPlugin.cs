using Application.BankActivity.Enums;
using Application.DTOs.InternalCommand;

using Domain.Common;
using Domain.Entities.BankAccount;
using Domain.Entities.BankAccountActivity;
using Domain.Entities.Currency;
using Domain.Entities.Integrator;

using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Xml;
using System.Xml.Serialization;

namespace MeDirect.ExchangeRatesApi
{
    public class ExchangeRatesApiPlugin
    {
        public PluginInfo GetCurencyInfo()
        {
            return new PluginInfo()
            {
                Name = "EXCHANGERATESAPI",
                Logo = null,
                GreyLogo = null
            };
        }

        public string UID()
        {
            return "76DE1DC0-03D3-4AA5-BF19-A91C60EE1111";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="integrator"></param>
        /// <param name="integratorType"></param>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public List<InternalCommandCurrencyActivityRequest> UpdateCurrencyActivityTransaction(BankAccount bankAccount,
            Currency currencyBase,
            List<Currency> currencies,
            Integrator integrator,
            DateTime startDate,
            DateTime endDate)
        {
            List<InternalCommandCurrencyActivityRequest> resultBLL = new List<InternalCommandCurrencyActivityRequest>();

            try
            {
                string username = integrator.UserName;
                string password = integrator.Password;
                string baseUrl = integrator.Url;

                var client = new RestClient(baseUrl);

                string symbolBase = currencyBase.Code;

                string symbol = "";
                if (currencies.Count == 0)
                    symbol = "";
                
                if (currencies.Count == 1)
                    symbol = currencies[0].Code;
                
                if(currencies.Count > 1)
                    symbol = string.Join("%2C", currencies.Select(x=> x.Code).ToArray());

                var requestUrl = $"exchangerates_data/latest?symbols={symbol}&base={symbolBase}";

                var request = new RestRequest(requestUrl, Method.Get);
                request.AddHeader("apikey", password);

                var response = client.Execute(request);
                
                if(response.IsSuccessStatusCode)
                {
                    JObject jObject = JObject.Parse(response.Content);

                    var ratesCount = jObject["rates"].Count();
                    for (int i = 0; i < ratesCount; i++)
                    {
                        InternalCommandCurrencyActivityRequest newItem = new InternalCommandCurrencyActivityRequest();

                        newItem.SymbolBase = symbolBase;
                        newItem.Symbol = ((Newtonsoft.Json.Linq.JProperty)jObject["rates"].ToList()[i]).Name;
                        newItem.Date = jObject["date"].Value<string>();
                        newItem.Timestamp = jObject["timestamp"].Value<int>();
                        newItem.Rate = jObject["rates"].ToList()[i].First().Value<string>();

                        resultBLL.Add(newItem);
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultBLL;
        }
    }
}
