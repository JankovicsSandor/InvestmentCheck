using InvestmentCheck.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentCheck
{
    public class ViewModel
    {
        public BindingList<Investment> InvestmentList { get; set; }

        public ViewModel()
        {
            this.InvestmentList = new BindingList<Investment>();
        }
    }
}
