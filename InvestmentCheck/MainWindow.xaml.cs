using InvestmentCheck.BussinessLogic;
using InvestmentCheck.BussinessLogic.SaveInvestments;
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
using System.Windows.Threading;

namespace InvestmentCheck
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ViewModel mainWindowModel;
        private IFileOperationBussinessLogic _fileOperator;

        public MainWindow()
        {
            _fileOperator = new FileOperationBussinessLogic();
            // TODO eliminate depencies VM shoud not know about dependencies
            mainWindowModel = new ViewModel(new RefreshPriceBussinessLogic(), _fileOperator);

            this.DataContext = mainWindowModel;
            InitializeComponent();

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (mainWindowModel.RemainingTimeUntilUpdate == 1)
            {
                mainWindowModel.refreshPrice();
            }
            mainWindowModel.RemainingTimeUntilUpdate -= 1;

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
                        InvestedAmount = resultData.InvestmentTotal,
                        PricePerCoin = double.Parse(resultData.PricePerCoin)
                    };
                    mainWindowModel.AddNewInvestment(newItem);

                    _fileOperator.SaveInvestmentListToFile(mainWindowModel.InvestmentList);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            mainWindowModel.ShowProgressBar = true;
            // TODO create an asnyc task to avoid blocking call
            IEnumerable<Investment> savedInvestments = _fileOperator.LoadInvestmentList();
            foreach (var invest in savedInvestments)
            {
                mainWindowModel.AddNewInvestment(invest);
            };
            mainWindowModel.ShowProgressBar = false;
            mainWindowModel.RefreshListPriceCommand.Execute(null);

        }
    }
}
