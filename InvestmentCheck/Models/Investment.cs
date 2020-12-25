using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentCheck.Models
{
    public class Investment : Bindable
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string CoinType { get; set; }

        private bool isNegative;

        [JsonIgnore]
        public bool IsNegative
        {
            get { return isNegative; }
            set
            {
                SetProperty(ref isNegative, value);
                OnPropertyChanged(nameof(isNegative));
            }
        }


        private double currentPrice;

        [JsonIgnore]
        public double CurrentPrice
        {
            get { return currentPrice; }
            set
            {
                SetProperty(ref currentPrice, value);
                OnPropertyChanged(nameof(currentPrice));
                this.IsNegative = pricePerCoin < currentPrice;
            }
        }


        private double pricePerCoin;

        /// <summary>
        /// Gets or sets the price of the coin at the time of investment
        /// </summary>
        public double PricePerCoin
        {
            get { return pricePerCoin; }
            set
            {
                SetProperty(ref pricePerCoin, value);
                OnPropertyChanged(nameof(pricePerCoin));
            }
        }

        private double investmentValue;

        /// <summary>
        /// Gets or sets the actual value of investment
        /// </summary>
        public double InvestmentValue
        {
            get
            {
                return this.Amount * this.pricePerCoin;
            }
            set
            {
                SetProperty(ref investmentValue, value);
                OnPropertyChanged(nameof(investmentValue));
            }
        }

        private int investAmount;

        /// <summary>
        /// Gets or sets the total price invested (coin price+ transaction fee)
        /// </summary>
        public int InvestedAmount
        {
            get { return investAmount; }
            set
            {
                SetProperty(ref investAmount, value);
                OnPropertyChanged(nameof(investAmount));
            }
        }

        public Investment()
        {
        }
    }
}
