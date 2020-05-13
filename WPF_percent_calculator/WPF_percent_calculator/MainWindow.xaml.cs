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

namespace WPF_percent_calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private double errorCheck = 0;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearText(TextBox txtbx)
        {
            txtbx.Text = "";
            txtbx.Opacity = 1f;
            txtbx.FontStyle = FontStyles.Normal;
        }


        private void CalculateBtn_Click(object sender, RoutedEventArgs e)
        {
            //Try to convert number to double, need at least 2 field
            double amount = ConvertToDouble(AmountTxtBx.Text);
            double percentage = ConvertToDouble(PercentTxtBx.Text);
            double result = ConvertToDouble(ResultTxtBx.Text);

            double[] parsedResult = { amount, percentage, result };
            int check = 0;

            for(int i=0;i<parsedResult.Length;i++)
            {
                if( parsedResult[i] == errorCheck)
                {
                    check++;
                }
            }

            if (check >= 2)
            {
                MessageBox.Show("Need at least two fields");
                return;
            }

            //Magic!
            parsedResult[1] = percentage / 100; //to get the decimal percentage value
            WhichCalculation(parsedResult);
            
        }

        private void WhichCalculation(double[] parsedResult)
        {
            int calculateMe = -1;
            for(int i = 0; i < parsedResult.Length; i++)
            {
                if (parsedResult[i] == 0)
                {
                    calculateMe = i;
                    break;
                }
            }

            // For ease
            double amount = parsedResult[0];
            double percent = parsedResult[1];
            double result = parsedResult[2];

            switch (calculateMe)
            {
                case 0:
                    // amount = result/percent
                    amount = result / percent;
                    AmountTxtBx.Text = amount.ToString();
                    break;
                case 1:
                    // percentage = result / amount
                    percent = result / amount;
                    percent *= 100;
                    PercentTxtBx.Text = percent.ToString() + "%";
                    break;
                case 2:
                    // result = percent * amount
                    result = percent * amount;
                    ResultTxtBx.Text = result.ToString();
                    break;
                default: //something's off
                    MessageBox.Show("Something's off, check your input");
                    break;
            }
        }

        private double ConvertToDouble(string value)
        {
            double parsedNumber = 0;
            if(!Double.TryParse(value, out parsedNumber))
            {
                parsedNumber = errorCheck;
            }
            return parsedNumber;
        }

        private void AmountTxtBx_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearText(sender as TextBox);
        }

        private void Percent_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearText(sender as TextBox);
        }

        private void ResultTxtBx_GotFocus(object sender, RoutedEventArgs e)
        {
            ClearText(sender as TextBox);
        }
    }
}
