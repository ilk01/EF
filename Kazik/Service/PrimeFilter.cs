
using System.IO;

namespace Kazik.Service;

public static class PrimeFilter
{
    private static readonly Mutex Mutex = new();

    public static async Task FilterNumber(IProgress<int> progress)
    {
        try
        {
            if (Mutex.WaitOne())
            {
                try
                {
                    if (!File.Exists("numbers.txt") || new FileInfo("numbers.txt").Length == 0)
                    {
                        var generatedNumbers = await NumberGenerator.Generate(progress);
                        await File.WriteAllLinesAsync("numbers.txt", generatedNumbers.Select(n => n.ToString()));
                    }

                    var numbers = await File.ReadAllLinesAsync("numbers.txt");
                    var primeNumbers = numbers.Select(int.Parse).Where(IsPrime).ToList();

                    var total = primeNumbers.Count;
                    for (var i = 0; i < total; i++)
                    {
                        progress.Report((int)((i + 1) / (float)total * 100));
                        await Task.Delay(50); 
                    }

                    await File.WriteAllLinesAsync("primes.txt", primeNumbers.Select(n => n.ToString()));
                }
                finally
                {
                    Mutex.ReleaseMutex();
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка в Filter: " + ex.Message);
        }
    }


    private static bool IsPrime(int number)
    {
        if (number <= 1) return false;
        for (var i = 2; i <= Math.Sqrt(number); i++)
        {
            if (number % i == 0) return false;
        }
        return true;
    }
}