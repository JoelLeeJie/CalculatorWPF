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

namespace CalculatorWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private string resultString = "";
        char[] operatorArray = new char[] {'+', '-', '*', '/', '%'};
        List<string> operatorInput = new List<string>() { };
        public MainWindow()
        {
            InitializeComponent();
        }
        

        private void MathButton(object sender, RoutedEventArgs e) //called when number/operator button is pressed.
        {
            TextBlock buttonText = (TextBlock)(sender as Button).Content;
            resultString = resultString + buttonText.Text;
            foreach(char i in operatorArray)
            {
                if (buttonText.Text.Contains(i)) operatorInput.Add(i.ToString());
            }
            ChangeResult(resultString);
        }

        private void GetResult(object sender, RoutedEventArgs e) //gets the string, splits it into just a number list. Calls Calculate, and displays result.
        {
            if (resultString == "") return;
            List<string> numberList = resultString.Split(operatorArray).ToList<string>(); //the operators, used as delimiters, will be removed.
            resultString = Calculate(numberList, operatorInput).ToString();
            ChangeResult(resultString);

        }

        private double Calculate(List<string> numbers, List<string> operators) //takes the number list, operator list and returns result.
        {
            double currentNumber = double.Parse(numbers[0]);
            int operatorIndex = 0;
            for(int index = 1; index<numbers.Count; index++) //starts at the second number.
            {
                switch(operators[operatorIndex]) 
                {
                    case "+": currentNumber += double.Parse(numbers[index]);
                        break; 
                    case "-": currentNumber -= double.Parse(numbers[index]);
                        break;
                    case "*": currentNumber *= double.Parse(numbers[index]);
                        break;
                    case "/": currentNumber /= double.Parse(numbers[index]);
                        break;
                    case "%": currentNumber %= double.Parse(numbers[index]);
                        break;
                    default: currentNumber = 1000000000000000000;
                        break;
                }
                operatorIndex++;
            }
            operatorInput = new List<string>(); //clears the operatorInput.
            return currentNumber;
        }

        private void ChangeResult(string output)
        {
            ResultBox.Text = output;
        }

        private void ClearResult(object sender, RoutedEventArgs e)
        {
            resultString = "";
            ResultBox.Text = resultString;
        }
    }

}

