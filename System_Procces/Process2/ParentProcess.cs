using System.Diagnostics;

namespace System_Procces.Process2;

class ParentProcess
{
    static void Main()
    {
        Console.WriteLine("Введите первое число:");
        var num1 = Console.ReadLine();
        Console.WriteLine("Введите второе число:");
        var num2 = Console.ReadLine();
        Console.WriteLine("Введите операцию (+, -, *, /):");
        var operation = Console.ReadLine();

        Console.WriteLine("1. Ожидать завершения дочернего процесса");
        Console.WriteLine("2. Принудительно завершить дочерний процесс");
        var choice = Console.ReadLine();

        ProcessStartInfo processStartInfo = new ProcessStartInfo
        {   
            FileName = @"C:\Users\User\RiderProjects\System_Procces\Child_Process\bin\Debug\net8.0\Child_Process.exe",
            Arguments = $"{num1} {num2} {operation}"
        };

        try
        {
            using Process? childProcess = Process.Start(processStartInfo);
            if (childProcess == null)
            {
                Console.WriteLine("Не удалось запустить дочерний процесс.");
                return;
            }

            switch (choice)
            {
                case "1":
                    childProcess.WaitForExit();
                    Console.WriteLine("Дочерний процесс завершен");
                    break;
                case "2":
                    childProcess.Kill();
                    Console.WriteLine("Дочерний процесс был принудительно завершен");
                    break;
                default:
                    Console.WriteLine("Неизвестный выбор");
                    break;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка запуска дочернего процесса: {ex.Message}");
        }
    }
}