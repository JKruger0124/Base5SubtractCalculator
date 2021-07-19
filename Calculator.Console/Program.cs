using Calculator.Lib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.WriteLine("Console Calculator - C#\r");
            System.Console.WriteLine("------------------------\n");

            SelectCalculator();
        }

        private static void SelectCalculator()
        {
            System.Console.WriteLine("Choose calculator:");
            System.Console.WriteLine("\ta - Simple");
            System.Console.WriteLine("\tb - Base5Subtract");

            switch (System.Console.ReadLine())
            {
                case "a":
                    SimpleCalculator();
                    break;
                case "b":
                    Base5SubtractCalculator();
                    break;
                default:
                    System.Console.WriteLine("Incorrect selection");
                    break;
            }

            SelectCalculator();
        }

        private static void SimpleCalculator()
        {
            var calculator = new SimpleCalculator();

            try
            {
                // First number.
                System.Console.WriteLine("Type a number, and then press Enter");
                var number1 = float.Parse(System.Console.ReadLine());

                // Second number.
                System.Console.WriteLine("Type another number, and then press Enter");
                var number2 = float.Parse(System.Console.ReadLine());

                System.Console.WriteLine("Select operator:");
                System.Console.WriteLine("\ta - Add");
                System.Console.WriteLine("\ts - Subtract");

                switch (System.Console.ReadLine())
                {
                    case "a":
                        System.Console.WriteLine(string.Format("Your result: {0}", calculator.Calculate(Operation.Add, number1, number2)));
                        break;
                    case "s":
                        System.Console.WriteLine(string.Format("Your result: {0}", calculator.Calculate(Operation.Subtract, number1, number2)));
                        break;
                    default:
                        System.Console.WriteLine("Incorrect selection");
                        break;
                }
            }
            catch (FormatException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

            System.Console.WriteLine("------------------------\n");

            SimpleCalculator();
        }

        private static void Base5SubtractCalculator()
        {
            var calculator = new Base5SubtractCalculator();

            // Type the number to subtract from 5.
            System.Console.WriteLine("Type a number to subtract from 5, and then press Enter");

            try
            {
                var value = float.Parse(System.Console.ReadLine());
                var result = calculator.Calculate(Operation.Subtract, null, value);

                if (result.HasValue)
                    System.Console.WriteLine(string.Format("Your result: {0}", result.Value));
                else
                {
                    foreach (var message in calculator.ValidationMessages)
                    {
                        System.Console.WriteLine(message.Message);
                    }
                }
            }
            catch (FormatException ex)
            {
                System.Console.WriteLine(ex.Message);
            }
            catch (Exception)
            {
                throw;
            }

            System.Console.WriteLine("------------------------\n");

            Base5SubtractCalculator();
        }
    }
}
