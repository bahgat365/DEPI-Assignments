using Session_09;
using System;
using System.Collections.Generic;
using System.Text;

namespace Session09Tests
{
    public class CalculatorTests
    {
        private readonly CalculatorApp _calculator;
        public CalculatorTests()
        {
            _calculator = new CalculatorApp();
        }
        [Fact]
        public void Add_ShouldReturnCorrectSum()
        {
            // AAA (Arrage - Act - Assert)

            // Arrage
            int a = 5;
            int b = 3;

            // Act
            var result = _calculator.Add(a, b);

            // Assert
            Assert.Equal(8, result);
        }

        [Fact]
        public void Add_ShouldReturnNotEqual()
        {
            // AAA

            // Arrange
            int a = 5;
            int b = 3;

            // Act
            var result = _calculator.Add(a, b);

            // Assert
            Assert.NotEqual(3, result);
        }

        [Fact]
        public void Subtract_ShouldReturnCorrectDifference()
        {
            // Arrange
            int a = 10;
            int b = 4;

            // Act 
            var result = _calculator.Subtract(a, b);

            // Assert
            Assert.Equal(6, result);
        }

        [Theory]
        [InlineData(2, 3, 6)]
        [InlineData(4, 5, 20)]
        [InlineData(-2, 3, -6)]
        public void Multiply_ShouldRetrunCorrectMultiplicationResult(int a, int b, int expected)
        {
            // Arrange
            // Act
            var result = _calculator.Multiply(a, b);

            // Assert
            Assert.Equal(expected, result);
        }

        [Theory]
        [InlineData(2, 3, 2)]
        [InlineData(4, 5, 120)]
        [InlineData(-2, 3, -16)]
        public void Multiply_ShouldReturnNotEqual(int a, int b, int expected)
        {
            // Arrange
            // Act
            var result = _calculator.Multiply(a, b);

            // Assert
            Assert.NotEqual(expected, result);
        }

        [Fact]
        public void Divide_ByZero_ShouldThrowException()
        {
            // Arrange
            int a = 12;
            int b = 0;
            //// Act
            //var result = _calculator.Divide(a, b);
            // Act & Assert

            Assert.Throws<DivideByZeroException>(() =>
            {
                return _calculator.Divide(a, b);
            });
        }
    }
}
