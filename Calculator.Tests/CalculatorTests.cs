using System;
using Calculator.Lib;
using NUnit.Framework;

namespace Calculator.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void TestBase5CalculatorSubtract()
        {
            var calculator = new Base5SubtractCalculator();
            var result = calculator.Calculate(Operation.Subtract, null, 3);

            Assert.AreEqual(2, result);
        }

        [Test]
        public void TestBase5CalculatorSubtractZero()
        {
            var calculator = new Base5SubtractCalculator();
            var result = calculator.Calculate(Operation.Subtract, null, 0);

            Assert.AreEqual(result, 5);
        }

        [Test]
        public void TestBase5CalculatorSubtractNegativeNumber()
        {
            var calculator = new Base5SubtractCalculator();
            var result = calculator.Calculate(Operation.Subtract, null, -5);

            Assert.AreEqual(10, result);
        }

        [Test]
        public void TestBase5CalculatorSubtractCloseToBase()
        {
            var calculator = new Base5SubtractCalculator();
            var result = calculator.Calculate(Operation.Subtract, null, 4.99);

            Assert.AreEqual(0.01, result);
        }

        [Test]
        public void TestBase5CalculatorValueIsNotNull()
        {
            var calculator = new Base5SubtractCalculator();
            var result = calculator.Calculate(Operation.Subtract, null, null);

            Assert.AreEqual(null, result);
            Assert.IsTrue(calculator.ValidationMessages.ContainsType(typeof(Calculator.Lib.Base5SubtractCalculator.ValidateValueIsNotNull)));
        }

        [Test]
        [ExpectedException(typeof(FormatException))]
        public void TestBase5CalculatorValueIsNumber()
        {
            var calculator = new Base5SubtractCalculator();
            var result = calculator.Calculate(Operation.Subtract, null, double.Parse("a"));

            Assert.AreEqual(null, result);
            Assert.IsTrue(calculator.ValidationMessages.ContainsType(typeof(Calculator.Lib.Base5SubtractCalculator.ValidateValueIsNumber)));
        }

        [Test]
        public void TestBase5CalculatorValueIsLessThanFive()
        {
            var calculator = new Base5SubtractCalculator();
            var result = calculator.Calculate(Operation.Subtract, null, 10);

            Assert.AreEqual(null, result);
            Assert.IsTrue(calculator.ValidationMessages.ContainsType(typeof(Calculator.Lib.Base5SubtractCalculator.ValidateValueIsLessThanFive)));
        }
    }
}
