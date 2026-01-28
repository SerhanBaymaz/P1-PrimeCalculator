namespace PrimeCalculator.Logic;

public class PrimeCalculator
{
    public static bool IsPrime(int number)
    {
        if (number < 2)
            return false;
        if (number == 2)
            return true;
        if (number % 2 == 0)
            return false;

        int boundary = (int)Math.Sqrt(number);
        for (int i = 3; i <= boundary; i += 2)
        {
            if (number % i == 0)
                return false;
        }
        return true;
    }

    public static List<int> GetPrimesUpTo(int maxNumber)
    {
        var primes = new List<int>();
        for (int i = 2; i <= maxNumber; i++)
        {
            if (IsPrime(i))
            {
                primes.Add(i);
            }
        }
        return primes;
    }
}
