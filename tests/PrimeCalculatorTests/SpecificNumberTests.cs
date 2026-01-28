using PrimeCalculator.Logic;

namespace PrimeCalculatorTests;

public class SpecificNumberTests
{
    [Fact]
    public void Test_99999_IsNotPrime()
    {
        // 99999 = 3 × 3 × 41 × 271 = 9 × 11111
        // So it should not be prime
        var result = PrimeCalculator.Logic.PrimeCalculator.IsPrime(99999);
        Assert.False(result, "99999 should not be prime as it equals 3² × 41 × 271");
    }

    [Fact]
    public void Test_Factorization_Of_99999()
    {
        // Let's verify that 99999 has the factors we expect
        Assert.True(99999 % 3 == 0, "99999 should be divisible by 3");
        Assert.True(99999 % 9 == 0, "99999 should be divisible by 9");
        Assert.True(99999 % 41 == 0, "99999 should be divisible by 41");
        Assert.True(99999 % 271 == 0, "99999 should be divisible by 271");
    }

    [Theory]
    [InlineData(99991, true)]   // This should be prime
    [InlineData(99989, true)]   // This should be prime  
    [InlineData(99999, false)]  // This should not be prime
    public void Test_Numbers_Around_99999(int number, bool expected)
    {
        var result = PrimeCalculator.Logic.PrimeCalculator.IsPrime(number);
        Assert.Equal(expected, result);
    }
}