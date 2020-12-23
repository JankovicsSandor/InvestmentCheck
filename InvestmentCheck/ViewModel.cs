using GalaSoft.MvvmLight.Command;
using InvestmentCheck.BussinessLogic;
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
    public class ViewModel
    {
        private IRefreshPriceBussinessLogic _refreshLogic;

        public BindingList<Investment> InvestmentList { get; set; }

        public ICommand RefreshListPriceCommand { get; private set; }

        public ViewModel(IRefreshPriceBussinessLogic refreshDataLogic)
        {
            _refreshLogic = refreshDataLogic;
            RefreshListPriceCommand = new RelayCommand(async () =>
            {
                await _refreshLogic.UpdatePrice(InvestmentList);
            });
            this.InvestmentList = new BindingList<Investment>();
        }
    }
}
