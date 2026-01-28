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

    /// <summary>
    /// Eratosthenes Kalburu algoritmasý - Büyük sayýlar için çok daha hýzlý
    /// </summary>
    public static List<int> GetPrimesUpToFast(int maxNumber)
    {
        if (maxNumber < 2)
            return new List<int>();

        // Boolean array oluþtur (true = asal olabilir)
        bool[] isPrime = new bool[maxNumber + 1];
        for (int i = 2; i <= maxNumber; i++)
        {
            isPrime[i] = true;
        }

        // Eratosthenes Kalburu
        int boundary = (int)Math.Sqrt(maxNumber);
        for (int i = 2; i <= boundary; i++)
        {
            if (isPrime[i])
            {
                // i'nin katlarýný iþaretle (asal deðil)
                for (int j = i * i; j <= maxNumber; j += i)
                {
                    isPrime[j] = false;
                }
            }
        }

        // Asal sayýlarý ekle
        var primes = new List<int>();
        for (int i = 2; i <= maxNumber; i++)
        {
            if (isPrime[i])
            {
                primes.Add(i);
            }
        }

        return primes;
    }
}
