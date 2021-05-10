using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockPriceMonitor.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;


namespace MainStockMonitoring.StockServices
{
    public class StockHttpClient
    {
        private readonly HttpClient _HttpClient;
        private string _BaseAddress { get; set; }
        /// <summary>
        /// Initialize Httplient
        /// </summary>
        public StockHttpClient()
        {

            if (this._HttpClient != null)
            {
                this._HttpClient.Dispose();
            }
            this._HttpClient = new HttpClient();
            //Yahoo finance base Addresse
            this._HttpClient.BaseAddress = new Uri("https://query2.finance.yahoo.com/v7/finance/options/");

            this._HttpClient.DefaultRequestHeaders.Accept.Clear();
            this._HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Gets stock´s information fron Yahoo Finance
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public StockModel GetStockInfo(string request)
        {
            StockModel stockModel = new StockModel();
            try
            {
                HttpResponseMessage httpResponseMessage = this._HttpClient.GetAsync($"{request}").Result;
                if (httpResponseMessage.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    //Extract dynamicaly Promise from Content
                    var response = httpResponseMessage.Content.ReadAsStringAsync();
                    dynamic jsonparse = JObject.Parse(response.Result);                   
                    var optionChain = jsonparse["optionChain"];
                    var result = optionChain["result"];
                    var tet3 = result[0];
                    var options = tet3["options"];

                    var tet5 = options[0];
                    var calls = tet5["calls"];
                    var callsArray = calls[0];
                    stockModel.StockPrice = callsArray["lastPrice"];
                    stockModel.StockName = request;
                    //JsonConvert.DeserializeObject<StockModel>(jsonparse.Result);
                    return stockModel;
                }                
            }
            catch
            {
                return null;
            }

            return stockModel;
        }

    }
}
