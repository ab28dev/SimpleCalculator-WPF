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

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber;
        double result;
        bool equalsClicked;

        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();
            
            acButton.Click += AcButton_Click;
            plusMinusButton.Click += PlusMinusButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalsButton.Click += EqualsButton_Click;
        }

        private void EqualsButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;
            expressionLabel.Content = $"{expressionLabel.Content}=";
            if (double.TryParse(resultLabel.Content.ToString(), out newNumber))
            {
                switch(selectedOperator)
                {
                    case SelectedOperator.Addtion:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Sub(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Mul(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Div(lastNumber, newNumber);
                        break;
                }
                
                resultLabel.Content = result.ToString();

                equalsClicked = true;
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            
            double tempNumber;
            expressionLabel.Content = $"{expressionLabel.Content}%";
            if (double.TryParse(resultLabel.Content.ToString(), out tempNumber))
            {
                tempNumber = tempNumber / 100;
                if (lastNumber != 0)
                    tempNumber *= lastNumber;

                resultLabel.Content = tempNumber.ToString();
            }
        }

        private void PlusMinusButton_Click(object sender, RoutedEventArgs e)
        {
            if(double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                lastNumber = lastNumber * -1;
                resultLabel.Content = lastNumber.ToString();
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLabel.Content = "0";
            expressionLabel.Content = "";
            result = 0;
            lastNumber = 0;

        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if(equalsClicked == true)
            {
                expressionLabel.Content = resultLabel.Content.ToString();
                equalsClicked = false;
            }
            string selectedValue = (sender as Button).Content.ToString();
            expressionLabel.Content = $"{expressionLabel.Content}{selectedValue}";

            if (double.TryParse(resultLabel.Content.ToString(), out lastNumber))
            {
                resultLabel.Content = "0";
            }

            if (sender == multiplyButton)
                selectedOperator = SelectedOperator.Multiplication;
            else if (sender == plusButton)
                selectedOperator = SelectedOperator.Addtion;
            else if (sender == minusButton)
                selectedOperator = SelectedOperator.Subtraction;
            else if (sender == divideButton)
                selectedOperator = SelectedOperator.Division;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            if (equalsClicked == true)
            {
                expressionLabel.Content = "";
                resultLabel.Content = "0";
                equalsClicked = false;
            }

            int selectedValue = int.Parse((sender as Button).Content.ToString());
            expressionLabel.Content = $"{expressionLabel.Content}{selectedValue}";
            if (resultLabel.Content.ToString() == "0")
            {
                resultLabel.Content = $"{selectedValue}";
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}{selectedValue}";
            }
        }


        public enum SelectedOperator
        {
            Addtion,
            Subtraction,
            Multiplication,
            Division
        }

        private void pointButton_Click(object sender, RoutedEventArgs e)
        {
            if (resultLabel.Content.ToString().Contains("."))
            {
                // do nothing
            }
            else
            {
                resultLabel.Content = $"{resultLabel.Content}.";
                expressionLabel.Content = $"{expressionLabel.Content}.";
            }
        }

        public class SimpleMath
        {
            public static double Add(double n1, double n2)
            {
                return n1 + n2;
            }
            public static double Sub(double n1, double n2)
            {
                return n1 - n2;
            }
            public static double Mul(double n1, double n2)
            {
                return n1 * n2;
            }
            public static double Div(double n1, double n2)
            {
                if(n2 == 0)
                {
                    MessageBox.Show("Division by 0 is not supported", "Wrong Operation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return 0;
                }
                return n1 / n2;
            }
        }
    }
}
