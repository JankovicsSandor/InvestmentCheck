using InvestmentCheck.Models;
using InvestmentCheck.Models.ApiResponse;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentCheck.BussinessLogic
{
    public class RefreshPriceBussinessLogic : IRefreshPriceBussinessLogic
    {
        private const string COINBASE_URL = "https://api.coinbase.com/v2/";
        private Dictionary<string, double> priceDictionary;
        public RefreshPriceBussinessLogic()
        {
            priceDictionary = new Dictionary<string, double>();
        }

        private async Task<PriceModel> GetPriceForCoinType(string coinType)
        {
            using (HttpClient coinbaseApiClient = CreateCoinbaseHttpClient())
            {
                HttpResponseMessage priceRequest = await coinbaseApiClient.GetAsync($"prices/{coinType}-EUR/spot");
                if (priceRequest.IsSuccessStatusCode)
                {
                    return JsonConvert.DeserializeObject<PriceModel>(await priceRequest.Content.ReadAsStringAsync());
                }
                return null;
            }
        }

        private HttpClient CreateCoinbaseHttpClient()
        {
            HttpClient coinClient = new HttpClient();
            coinClient.BaseAddress = new Uri(COINBASE_URL);
            return coinClient;
        }

        //TODO move it as a background process
        public async Task UpdatePrice(IEnumerable<Investment> investments)
        {
            foreach (var priceItem in priceDictionary.ToList())
            {
                PriceModel priceModel = await GetPriceForCoinType(priceItem.Key);
                priceDictionary[priceItem.Key] = double.Parse(priceModel.Data.Amount);
            }
            IEnumerable<IGrouping<string, Investment>> coins = investments.GroupBy(e => e.CoinType);

            foreach (var oneCoinType in coins)
            {
                if (!priceDictionary.ContainsKey(oneCoinType.Key))
                {
                    PriceModel priceModel = await GetPriceForCoinType(oneCoinType.Key);
                    priceDictionary.Add(oneCoinType.Key, double.Parse(priceModel.Data.Amount));
                }
            }
            foreach (var investment in investments)
            {
                investment.CurrentPrice = priceDictionary[investment.CoinType];
            }
        }

    }
}
