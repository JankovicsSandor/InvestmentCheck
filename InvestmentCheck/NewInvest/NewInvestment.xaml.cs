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
using System.Windows.Shapes;

namespace InvestmentCheck.NewInvest
{
    /// <summary>
    /// Interaction logic for NewInvestment.xaml
    /// </summary>
    public partial class NewInvestment : Window
    {
        public NewInvestment()
        {
            InitializeComponent();
        }

        private void CloseNewInvestmentDialog(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }

        private void SaveInvestment(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
