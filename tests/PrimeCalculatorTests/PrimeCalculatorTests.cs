using PrimeCalculator.Logic;

namespace PrimeCalculatorTests;

public class PrimeCalculatorTests
{
    [Theory]
    [InlineData(2, true)]
    [InlineData(3, true)]
    [InlineData(5, true)]
    [InlineData(7, true)]
    [InlineData(11, true)]
    [InlineData(13, true)]
    [InlineData(17, true)]
    [InlineData(19, true)]
    [InlineData(23, true)]
    [InlineData(29, true)]
    public void IsPrime_WithPrimeNumbers_ReturnsTrue(int number, bool expected)
    {
        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.IsPrime(number);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(1, false)]
    [InlineData(0, false)]
    [InlineData(-1, false)]
    [InlineData(-5, false)]
    [InlineData(4, false)]
    [InlineData(6, false)]
    [InlineData(8, false)]
    [InlineData(9, false)]
    [InlineData(10, false)]
    [InlineData(12, false)]
    [InlineData(14, false)]
    [InlineData(15, false)]
    [InlineData(16, false)]
    [InlineData(18, false)]
    [InlineData(20, false)]
    [InlineData(21, false)]
    [InlineData(22, false)]
    [InlineData(24, false)]
    [InlineData(25, false)]
    public void IsPrime_WithNonPrimeNumbers_ReturnsFalse(int number, bool expected)
    {
        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.IsPrime(number);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(100, false)]
    [InlineData(121, false)] // 11^2
    [InlineData(169, false)] // 13^2
    [InlineData(289, false)] // 17^2
    public void IsPrime_WithLargeCompositeNumbers_ReturnsFalse(int number, bool expected)
    {
        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.IsPrime(number);

        // Assert
        Assert.Equal(expected, result);
    }

    [Theory]
    [InlineData(97, true)]
    [InlineData(101, true)]
    [InlineData(103, true)]
    [InlineData(107, true)]
    [InlineData(109, true)]
    public void IsPrime_WithLargePrimeNumbers_ReturnsTrue(int number, bool expected)
    {
        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.IsPrime(number);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithNumber2_ReturnsListWithOnly2()
    {
        // Arrange
        int maxNumber = 2;
        var expected = new List<int> { 2 };

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithNumber10_ReturnsCorrectPrimes()
    {
        // Arrange
        int maxNumber = 10;
        var expected = new List<int> { 2, 3, 5, 7 };

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithNumber20_ReturnsCorrectPrimes()
    {
        // Arrange
        int maxNumber = 20;
        var expected = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19 };

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithNumber30_ReturnsCorrectPrimes()
    {
        // Arrange
        int maxNumber = 30;
        var expected = new List<int> { 2, 3, 5, 7, 11, 13, 17, 19, 23, 29 };

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithNumber1_ReturnsEmptyList()
    {
        // Arrange
        int maxNumber = 1;
        var expected = new List<int>();

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        Assert.Empty(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithNumber0_ReturnsEmptyList()
    {
        // Arrange
        int maxNumber = 0;
        var expected = new List<int>();

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        Assert.Empty(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithNegativeNumber_ReturnsEmptyList()
    {
        // Arrange
        int maxNumber = -5;
        var expected = new List<int>();

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        Assert.Empty(result);
        Assert.Equal(expected, result);
    }

    [Fact]
    public void GetPrimesUpTo_WithLargerRange_ReturnsCorrectCount()
    {
        // Arrange
        int maxNumber = 100;

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        // There are 25 prime numbers between 1 and 100
        Assert.Equal(25, result.Count);
        
        // Verify first few and last few primes
        Assert.Equal(2, result[0]);
        Assert.Equal(3, result[1]);
        Assert.Equal(5, result[2]);
        Assert.Equal(97, result[^1]); // Last prime up to 100
        Assert.Equal(89, result[^2]); // Second to last prime up to 100
    }

    [Fact]
    public void GetPrimesUpTo_ResultsAreInAscendingOrder()
    {
        // Arrange
        int maxNumber = 50;

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        for (int i = 1; i < result.Count; i++)
        {
            Assert.True(result[i] > result[i - 1], 
                $"Prime numbers should be in ascending order. Found {result[i - 1]} followed by {result[i]}");
        }
    }

    [Fact]
    public void GetPrimesUpTo_AllResultsArePrime()
    {
        // Arrange
        int maxNumber = 50;

        // Act
        var result = PrimeCalculator.Logic.PrimeCalculator.GetPrimesUpTo(maxNumber);

        // Assert
        foreach (var number in result)
        {
            Assert.True(PrimeCalculator.Logic.PrimeCalculator.IsPrime(number), 
                $"Number {number} in the result should be prime");
        }
    }
}