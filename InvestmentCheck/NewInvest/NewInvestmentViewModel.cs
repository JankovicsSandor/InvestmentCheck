using InvestmentCheck.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvestmentCheck.NewInvest
{
    public class NewInvestmentViewModel : Bindable
    {
        public DateTime InvestmentTime { get; set; }
        public Array CoinTypeList
        {
            get
            {
                return Enum.GetValues(typeof(Cointype));
            }
        }

        public string SelectedCoin { get; set; }
        private string pricePerCoin;
        private string coinAmount;

        public string CoinAmount
        {
            get { return coinAmount; }
            set
            {
                SetProperty(ref coinAmount, value);
                OnPropertyChanged(nameof(InvestmentValue));
            }
        }

        public string PricePerCoin
        {
            get { return pricePerCoin; }
            set
            {
                SetProperty(ref pricePerCoin, value);
                OnPropertyChanged(nameof(InvestmentValue));
            }
        }

        public double InvestmentValue
        {
            get
            {
                if (string.IsNullOrEmpty(coinAmount) || string.IsNullOrEmpty(pricePerCoin))
                {
                    return 0;
                }
                return double.Parse(coinAmount) * double.Parse(pricePerCoin);
            }
        }

        public int InvestmentTotal { get; set; }

        public NewInvestmentViewModel()
        {
            this.InvestmentTime = DateTime.Now;
        }
    }
}
