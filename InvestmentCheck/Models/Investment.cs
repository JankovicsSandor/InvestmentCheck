﻿using System;
using System.Collections.Generic;
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

        public int InvestedAmound
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
