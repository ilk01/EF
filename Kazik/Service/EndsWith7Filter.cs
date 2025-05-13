using System.IO;


namespace Kazik.Service
{
    public static class EndsWith7Filter
    {
        private static readonly Mutex Mutex = new();

        public static async Task Filter()
        {
            try
            {
                if (Mutex.WaitOne())
                {
                    try
                    {
                        if (!File.Exists("numbers.txt") || new FileInfo("numbers.txt").Length == 0)
                        {
                            var generatedNumbers = await NumberGenerator.Generate(new Progress<int>());
                            await File.WriteAllLinesAsync("numbers.txt", generatedNumbers.Select(n => n.ToString()));
                        }

                        var numbers = await File.ReadAllLinesAsync("numbers.txt");

                        var endsWith7 = numbers.Where(n => n.EndsWith($"7")).ToList();

                        await Task.Delay(1000);

                        await File.WriteAllLinesAsync("endsWith7.txt", endsWith7);

                        Console.WriteLine($"Найдено {endsWith7.Count} чисел, заканчивающихся на 7.");
                    }
                    finally
                    {
                        Mutex.ReleaseMutex();
                    }
                }
                else
                {
                    Console.WriteLine("Не удалось захватить мьютекс.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в Filter: " + ex.Message);
            }
        }

    }
}