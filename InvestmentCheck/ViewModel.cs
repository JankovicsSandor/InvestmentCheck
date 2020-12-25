using GalaSoft.MvvmLight.Command;
using InvestmentCheck.BussinessLogic;
using InvestmentCheck.BussinessLogic.SaveInvestments;
using InvestmentCheck.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace InvestmentCheck
{
    public class ViewModel : Bindable
    {
        private int updateScheduleTimeInSec = 10;
        private IRefreshPriceBussinessLogic _refreshLogic;
        private bool showProgressbar;

        private int remainingTimeUntilUpdate;

        public int RemainingTimeUntilUpdate
        {
            get { return remainingTimeUntilUpdate; }
            set
            {
                if (value == 0)
                {
                    value = updateScheduleTimeInSec;
                }
                SetProperty(ref remainingTimeUntilUpdate, value);
                OnPropertyChanged(nameof(RemainingTimeUntilUpdate));
            }
        }

        /// <summary>
        /// Gets or sets to show the progress bar
        /// </summary>
        public bool ShowProgressBar
        {
            get { return showProgressbar; }
            set
            {
                SetProperty(ref showProgressbar, value);
                OnPropertyChanged(nameof(ShowProgressBar));
            }
        }

        private DateTime lastUpdateTime;

        /// <summary>
        /// Gets or sets the latest update time
        /// </summary>
        public DateTime LastUpdateTime
        {
            get { return lastUpdateTime; }
            set
            {
                SetProperty(ref lastUpdateTime, value);
                OnPropertyChanged(nameof(LastUpdateTime));
            }
        }

        private double totalInvestedValue;

        public double TotalInvestedValue
        {
            get { return totalInvestedValue; }
            set
            {
                SetProperty(ref totalInvestedValue, value);
                OnPropertyChanged(nameof(TotalInvestedValue));
            }
        }


        private double totalActualValue;

        public double TotalActualValue
        {
            get { return totalActualValue; }
            set
            {
                SetProperty(ref totalActualValue, value);
                OnPropertyChanged(nameof(TotalActualValue));
            }
        }

        public BindingList<Investment> InvestmentList { get; private set; }

        public ICommand RefreshListPriceCommand { get; private set; }

        public ICommand SaveFileListCommand { get; private set; }

        public void AddNewInvestment(Investment newInvestment)
        {
            this.InvestmentList.Add(newInvestment);
            this.TotalInvestedValue += newInvestment.InvestedAmount;
        }

        public ViewModel(IRefreshPriceBussinessLogic refreshDataLogic, IFileOperationBussinessLogic _fileOperator)
        {
            this.RemainingTimeUntilUpdate = updateScheduleTimeInSec;
            this.ShowProgressBar = false;
            _refreshLogic = refreshDataLogic;
            RefreshListPriceCommand = new RelayCommand(async () =>
            {
                await refreshPrice();
            });

            SaveFileListCommand = new RelayCommand(() =>
            {
                this.ShowProgressBar = true;
                _fileOperator.SaveInvestmentListToFile(InvestmentList);
                this.ShowProgressBar = false;
            });
            this.InvestmentList = new BindingList<Investment>();
        }

        public async Task refreshPrice()
        {
            this.ShowProgressBar = true;
            this.TotalActualValue = await _refreshLogic.UpdatePrice(InvestmentList);
            this.LastUpdateTime = DateTime.Now;
            this.ShowProgressBar = false;
        }
    }
}
