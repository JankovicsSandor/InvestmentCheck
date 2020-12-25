using InvestmentCheck.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InvestmentCheck.BussinessLogic
{
    public interface IRefreshPriceBussinessLogic
    {
        Task<double> UpdatePrice(IEnumerable<Investment> investments);
    }
}