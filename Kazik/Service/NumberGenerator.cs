
using System.IO;

namespace Kazik.Service
{
    public static class NumberGenerator
    {
        private static readonly Mutex Mutex = new();

        public static async Task<List<int>> Generate(IProgress<int> progress)
        {
            List<int> generatedNumbers = new List<int>();

            try
            {
                if (Mutex.WaitOne())
                {
                    try
                    {
                        Random random = new Random();
                        var totalNumbers = 50; 

                        if (!File.Exists("numbers.txt") || new FileInfo("numbers.txt").Length == 0)
                        {
                            for (var i = 0; i < totalNumbers; i++)
                            {
                                generatedNumbers.Add(random.Next(1, 100));
                                await Task.Delay(500);
                                progress.Report((int)((i + 1) / (float)totalNumbers * 100));
                            }

                            await File.WriteAllLinesAsync("numbers.txt", generatedNumbers.Select(n => n.ToString()));
                        }
                        else
                        {
                            var numbersFromFile = await File.ReadAllLinesAsync("numbers.txt");
                            generatedNumbers = numbersFromFile.Select(int.Parse).ToList();
                        }
                    }
                    finally
                    {
                        Mutex.ReleaseMutex();
                    }
                }
                else
                {
                    Console.WriteLine("Не удалось захватить мьютекс");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка в Generate: " + ex.Message);
            }

            return generatedNumbers;
        }

    }
}