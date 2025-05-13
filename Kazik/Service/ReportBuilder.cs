using System.IO;

namespace Kazik.Service
{
    public static class ReportBuilder
    {
        private static readonly Mutex Mutex = new();

        public static async Task Build(IProgress<int> progress) 
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

                        if (!File.Exists("primes.txt") || new FileInfo("primes.txt").Length == 0)
                        {
                            await PrimeFilter.FilterNumber(progress);
                        }

                        if (!File.Exists("endsWith7.txt") || new FileInfo("endsWith7.txt").Length == 0)
                        {
                            await EndsWith7Filter.Filter();
                        }

                        var numbers = await File.ReadAllLinesAsync("numbers.txt");
                        var primes = await File.ReadAllLinesAsync("primes.txt");
                        var endsWith7 = await File.ReadAllLinesAsync("endsWith7.txt");

                        var report = $"Всего чисел: {numbers.Length}\n" + 
                                     $"Простых чисел: {primes.Length}\n" +
                                     $"Чисел, заканчивающихся на 7: {endsWith7.Length}\n\n" +
                                     "Числа, заканчивающиеся на 7:\n" + string.Join("\n", endsWith7);

                        progress.Report(50); 
                        await Task.Delay(500);

                        await File.WriteAllTextAsync("report.txt", report);

                        progress.Report(100);
                    }
                    finally
                    {
                        Mutex.ReleaseMutex();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в Build: " + ex.Message);
            }
        }
    }
}
