using CodeFirst.Data.Contexts;
using CodeFirst.Data.Models;

namespace CodeFirst.Data.NavigationMenu;

public class CarMenu(ShowroomContext context)
{
    public void ShowMenu()
    {
        while (true) 
        {
            Console.WriteLine("1. Добавить новый автомобиль");
            Console.WriteLine("2. Удалить автомобиль");
            Console.WriteLine("3. Обновить данные автомобиля");
            Console.WriteLine("4. Вывести все автомобили");
            Console.WriteLine("5. Вернуться в главное меню");
            var choice = WriteRead(string.Empty);

            switch (choice)
            {
                case "1":
                    AddCar();
                    break;
                case "2":
                    DeleteCar();
                    break;
                case "3":
                    UpdateCar();
                    break;
                case "4":
                    ShowAllCars();
                    break;
                case "5":
                    return; 
            }
        }
    }

    private void AddCar()
    {
        var brand = WriteRead("Введите марку автомобиля: ");
        var model = WriteRead("Введите модель автомобиля: ");
        var year = int.Parse(WriteRead("Введите год автомобиля: "));
        var price = decimal.Parse(WriteRead("Введите цену автомобиля: "));
        var car = new Car(brand, model, year, price);
        context.Cars.Add(car);
        context.SaveChanges();
        Console.WriteLine("Автомобиль добавлен!");
    }

    private void DeleteCar()
    {
        var id = int.Parse(WriteRead("Введите ID автомобиля для удаления: "));
        var car = context.Cars.FirstOrDefault(c => c.Id == id);
        if (car != null)
        {
            context.Cars.Remove(car);
            context.SaveChanges();
            Console.WriteLine("Автомобиль удалён!");
        }
        else
        {
            Console.WriteLine("Автомобиль не найден");
        }
    }

    private void UpdateCar()
    {
        var id = int.Parse(WriteRead("Введите ID автомобиля для обновления: "));
        var car = context.Cars.FirstOrDefault(c => c.Id == id);
        if (car != null)
        {
            car.Brand = WriteRead("Введите новую марку автомобиля: ");
            car.Model = WriteRead("Введите новую модель автомобиля: ");
            car.Year = int.Parse(WriteRead("Введите новый год автомобиля: "));
            car.Price = decimal.Parse(WriteRead("Введите новую цену автомобиля: "));
            context.SaveChanges();
            Console.WriteLine(" Автомобиль обновлен!");
        }
        else
        {
            Console.WriteLine("Автомобиль не найден");
        }
    }

    private void ShowAllCars()
    {
        var cars = context.Cars.ToList();
        foreach (var car in cars)
        {
            Console.WriteLine($"ID: {car.Id}, Марка: {car.Brand}, Модель: {car.Model}, Год: {car.Year}, Цена: {car.Price}");
        }
    }

    private string WriteRead(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? string.Empty;
    }
}