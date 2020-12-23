using InvestmentCheck.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InvestmentCheck.BussinessLogic.SaveInvestments
{
    public class FileOperationBussinessLogic : IFileOperationBussinessLogic
    {
        private const string InvestmentFileName = "investment.txt";

        public void SaveInvestmentListToFile(IEnumerable<Investment> investmentList)
        {
            string serialized = JsonConvert.SerializeObject(investmentList);
            File.WriteAllText(InvestmentFileName, serialized);
        }

        public IEnumerable<Investment> LoadInvestmentList()
        {
            if (File.Exists(InvestmentFileName))
            {
                string rawContent = File.ReadAllText(InvestmentFileName);
                return JsonConvert.DeserializeObject<IEnumerable<Investment>>(rawContent);
            }
            return new List<Investment>();

        }
    }
}
