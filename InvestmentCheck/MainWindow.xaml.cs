using InvestmentCheck.Models;
using InvestmentCheck.NewInvest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace InvestmentCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel mainWindowModel;
        public MainWindow()
        {
            mainWindowModel = new ViewModel();
            this.DataContext = mainWindowModel;
            InitializeComponent();
        }

        private void CreateNewInvestment(object sender, RoutedEventArgs e)
        {
            NewInvestment dialog = new NewInvestment();

            if (dialog.ShowDialog() == true)
            {
                NewInvestmentViewModel resultData = (NewInvestmentViewModel)dialog.DataContext;
                if (resultData != null)
                {
                    Investment newItem = new Investment()
                    {
                        Amount = double.Parse(resultData.CoinAmount),
                        CoinType = resultData.SelectedCoin,
                        Date = resultData.InvestmentTime,
                        InvestedAmound = resultData.InvestmentTotal,
                        PricePerCoin = double.Parse(resultData.PricePerCoin)
                    };
                    mainWindowModel.InvestmentList.Add(newItem);
                }
                Console.WriteLine(5);
            }
        }
    }
}
