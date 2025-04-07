using async_await.LogicFunction;

namespace async_await.User;

public class UserInterface
{
    public static async Task ShowMenuAsync()
    {
        while (true)
        {
            Console.WriteLine("\nВыберите действие:");
            Console.WriteLine("1. Добавить студента (без async)");
            Console.WriteLine("2. Показать студентов (без async)");
            Console.WriteLine("3. Добавить студента (с async)");
            Console.WriteLine("4. Показать студентов (с async)");
            Console.WriteLine("0. Выйти");

            Console.Write("Ваш выбор: ");
            var choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Введите имя: ");
                    StudentService.AddStudentManually(Console.ReadLine());
                    break;
                case "2":
                    StudentService.ShowAllStudentsManually();
                    break;
                case "3":
                    Console.Write("Введите имя: ");
                    await StudentService.AddStudentAsync(Console.ReadLine());
                    break;
                case "4":
                    await StudentService.ShowAllStudentsAsync();
                    break;
                case "0":
                    return;
                default:
                    Console.WriteLine("Неверный выбор");
                    break;
            }
        }
    }
}