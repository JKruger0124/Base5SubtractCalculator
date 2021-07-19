using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator.Lib
{
    public enum Operation
    {
        Add, Subtract, Multiply, Divide
    }

    /// <summary>
    /// Base class for the different calculators with default operations
    /// The class includes validation, that every calculator must implement
    /// </summary>
    public abstract class Calculator
    {
        protected double? operandLeft;
        protected double? operandRight;

        /*This method could be optimized to use dynamic parameter, calling the correct calculation 
         * method based on the operation type.*/
        public virtual double? Calculate(Operation operation, double? x, double? y)
        {
            operandLeft = x;
            operandRight = y;

            switch (operation)
            {
                case Operation.Add:
                    {
                        return Add(operandLeft, operandRight);
                    }
                case Operation.Subtract:
                    {
                        return Subtract(operandLeft, operandRight);
                    }
                case Operation.Multiply:
                    {
                        return Multiply(operandLeft, operandRight);
                    }
                case Operation.Divide:
                    {
                        return Divide(operandLeft, operandRight);
                    }
                default: break;
            }

            return null;
        }

        protected double? Add(double? x, double? y)
        {
            if (IsValid())
                return Math.Round(operandLeft.Value + operandRight.Value, 2);

            return null;
        }

        protected double? Subtract(double? x, double? y)
        {
            if (IsValid())
                return Math.Round(operandLeft.Value - operandRight.Value, 2);

            return null;
        }

        /*This method will need to manage 0 dividend*/
        protected double? Divide(double? x, double? y)
        {
            if (IsValid())
                return Math.Round(operandLeft.Value / operandRight.Value, 2);

            return null;
        }

        protected double? Multiply(double? x, double? y)
        {
            if (IsValid())
                return Math.Round(operandLeft.Value * operandRight.Value, 2);

            return null;
        }

        public abstract bool IsValid();

        public IList<IValidation> Rules = new List<IValidation>();
        public IList<IValidation> ValidationMessages = new List<IValidation>();
    }

    public class SimpleCalculator : Calculator
    {
        public override bool IsValid()
        {
            return true;
        }
    }

    /// <summary>
    /// This calculator type will be instantiated with a leftOperand of 5
    /// overriding the base implementation of Subtract to ensure we always subtract input from 5
    /// </summary>
    public class Base5SubtractCalculator : Calculator
    {
        public Base5SubtractCalculator()
        {
            this.operandLeft = 5;

            Rules.Add(new ValidateValueIsNotNull());
            Rules.Add(new ValidateValueIsNumber());
            Rules.Add(new ValidateValueIsLessThanFive());
        }

        public override double? Calculate(Operation operation, double? x, double? y)
        {
            operandRight = y;
            return base.Calculate(operation, operandLeft, operandRight);
        }

        public override bool IsValid()
        {
            foreach (var rule in Rules)
            {
                if (!rule.IsValid(operandLeft, operandRight))
                    ValidationMessages.Add(rule);
            }

            return ValidationMessages.Count == 0;
        }

        public class ValidateValueIsNotNull : IValidation
        {
            public bool IsValid(double? operandLeft, double? operandRight)
            {
                return operandLeft != null && operandRight != null;
            }

            public string Message
            {
                get
                {
                    return "Please provide a value";
                }
            }
        }

        public class ValidateValueIsNumber : IValidation
        {
            public bool IsValid(double? operandLeft, double? operandRight)
            {
                return operandRight is double;
            }

            public string Message
            {
                get
                {
                    return "Please enter a number";
                }
            }
        }

        public class ValidateValueIsLessThanFive : IValidation
        {
            public bool IsValid(double? operandLeft, double? operandRight)
            {
                return operandRight < 5;
            }

            public string Message
            {
                get
                {
                    return "Value must be less than 5";
                }
            }
        }
    }

    /// <summary>
    /// Standard for Validations used in the calculators
    /// </summary>
    public interface IValidation
    {
        bool IsValid(double? operandLeft, double? operandRight);
        string Message { get; }
    }

    /// <summary>
    /// Extension method to check if validation results contain a specific type of Validation Type
    /// </summary>
    public static class Extensions
    {
        public static bool ContainsType(this IList<IValidation> collection, Type type)
        {
            return collection.Any(i => i.GetType() == type);
        }
    }
}
