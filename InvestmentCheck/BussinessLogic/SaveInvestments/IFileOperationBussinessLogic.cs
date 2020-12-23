using InvestmentCheck.Models;
using System.Collections.Generic;

namespace InvestmentCheck.BussinessLogic.SaveInvestments
{
    public interface IFileOperationBussinessLogic
    {
        IEnumerable<Investment> LoadInvestmentList();
        void SaveInvestmentListToFile(IEnumerable<Investment> investmentList);
    }
}