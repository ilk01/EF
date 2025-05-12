namespace Child_Process.Process1;

class ChildProcess
{
    static void Main(string[] args)
    {
        if (args.Length == 3)
        {
            var arg1 = args[0];
            var arg2 = args[1];
            var operation = args[2];

            if (int.TryParse(arg1, out var num1) && int.TryParse(arg2, out var num2))
            {
                double result;

                switch (operation)
                {
                    case "+":
                        result = num1 + num2;
                        Console.WriteLine($"Результат операции ({operation}): {num1} + {num2} = {result}");
                        break;
                    case "-":
                        result = num1 - num2;
                        Console.WriteLine($"Результат операции ({operation}): {num1} - {num2} = {result}");
                        break;
                    case "*":
                        result = num1 * num2;
                        Console.WriteLine($"Результат операции ({operation}): {num1} * {num2} = {result}");
                        break;
                    case "/":
                        if (num2 != 0)
                        {
                            result = (double)num1 / num2;
                            Console.WriteLine($"Результат операции ({operation}): {num1} / {num2} = {result}");
                        }
                        else
                        {
                            Console.WriteLine("Ошибка деление на ноль");
                        }
                        break;
                    default:
                        Console.WriteLine("Неизвестная операция");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Ошибк аргументы должны быть числами");
            }
        }
        else
        {
            Console.WriteLine("необходимо передать 3 аргумента (два числа и операция)");
        }
    }
}
