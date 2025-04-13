using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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

namespace calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double? lastNumber;
        double result;
        SelectedOperator selectedOperator;


        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            negativeButton.Click += NegativeButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            string input = resultLabel.Content.ToString().Replace(',', '.');
            if (double.TryParse(input, NumberStyles.Any, CultureInfo.InvariantCulture, out newNumber))
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber.Value, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber.Value, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber.Value, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber.Value, newNumber);
                        break;
                }
                resultLabel.Content = result.ToString();
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastNumber.HasValue)
            {
                if(double.TryParse(resultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedNumber))
                {
                    resultLabel.Content = parsedNumber / 100 * lastNumber.Value;
                }
            }
            else
            {
                if (double.TryParse(resultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedNumber))
                {
                    resultLabel.Content = parsedNumber / 100;
                }
            }
        }

        private void NegativeButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out double parsedNumber))
            {
                lastNumber = -parsedNumber;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            lastNumber = null;
            resultLabel.Content = "0";
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLabel.Content.ToString(), NumberStyles.Any, CultureInfo.InvariantCulture, out double parsedNumber))
            {
                lastNumber = parsedNumber;
                resultLabel.Content = "0";
            }

            if (sender == plusButton)
            {
                selectedOperator = SelectedOperator.Addition;
            }
            else if (sender == minusButton)
            {
                selectedOperator = SelectedOperator.Subtraction;
            }
            else if (sender == multiplicationButton)
            {
                selectedOperator = SelectedOperator.Multiplication;
            }
            else if (sender == divisionButton)
            {
                selectedOperator = SelectedOperator.Division;
            }
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = selectedValue.ToString();
            }
            else
            {
                resultLabel.Content += selectedValue.ToString();
            }
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLabel.Content.ToString().Contains("."))
            {
                resultLabel.Content += ".";
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double x, double y)
        {
            return x + y;
        }
        public static double Subtract(double x, double y)
        {
            return x - y;
        }
        public static double Multiply(double x, double y)
        {
            return x * y;
        }
        public static double Divide(double x, double y)
        {
            if(y == 0)
            {
                MessageBox.Show("Cannot divide by zero!", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
            return x / y;
        }
    }
}
