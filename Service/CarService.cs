using EFCore_log_and_task.Data;
using EFCore_log_and_task.Model;
using Microsoft.EntityFrameworkCore;


namespace EFCore_log_and_task.Service
{
    public class CarService
    {
        private readonly CarShopContext _context;

        public CarService(CarShopContext context)
        {
            _context = context;
        }
        
        private void DisplayCarsWithDealers()
        {
            var carsWithDealers = _context.Cars.Include(c => c.Dealer).ToList();

            foreach (var car in carsWithDealers)
            {
                Console.WriteLine($"{car.Make} {car.Model} ({car.Year}) - Дилер: {car.Dealer.Name}");
            }
        }

        private void DisplayCarWithDealerExplicitly()
        {
            var car = _context.Cars.First(); 
            _context.Entry(car).Reference(c => c.Dealer).Load(); 

            Console.WriteLine($"{car.Make} {car.Model} ({car.Year}) - Дилер: {car.Dealer.Name}");
        }

        private void DisplayCarsByMake(string? make)
        {
            if (make == null) return;
            var cars = _context.Cars.FromSqlRaw("SELECT * FROM Cars WHERE Make = {0}", make).ToList();
            foreach (var car in cars)
            {
                Console.WriteLine($"{car.Make} {car.Model} ({car.Year})");
            }
        }
        
        public void PerformCrudOperations()
        {
            while (true)
            {
                Console.WriteLine("\nВыберите операцию:");
                Console.WriteLine("1. Добавить новый автомобиль");
                Console.WriteLine("2. Обновить автомобиль");
                Console.WriteLine("3. Удалить автомобиль");
                Console.WriteLine("4. Просмотр всех автомобилей");
                Console.WriteLine("5. Отображение всех автомобилей с дилерами (Eager Loading)");
                Console.WriteLine("6. Отображение автомобиля с дилером (Explicit Loading)");
                Console.WriteLine("7. Выполнение SQL-запроса по марке автомобиля");
                Console.WriteLine("8. Добавить нового дилера");  
                Console.WriteLine("9. Выйти");
                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCar();
                        break;

                    case "2":
                        UpdateCar();
                        break;

                    case "3":
                        DeleteCar();
                        break;

                    case "4":
                        DisplayCars();
                        break;

                    case "5":
                        DisplayCarsWithDealers();
                        break;

                    case "6":
                        DisplayCarWithDealerExplicitly();
                        break;

                    case "7":
                        Console.WriteLine("Введите марку автомобиля для поиска:");
                        var make = Console.ReadLine();
                        DisplayCarsByMake(make);
                        break;

                    case "8": 
                        AddDealer();
                        break;

                    case "9":
                        return;

                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }

        
        private void AddDealer()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                Console.WriteLine("Введите имя дилера:");
                var dealerName = Console.ReadLine();
                Console.WriteLine("Введите местоположение дилера:");
                var dealerLocation = Console.ReadLine();

                var dealer = new Dealer
                {
                    Name = dealerName,
                    Location = dealerLocation,
                    Cars = new List<Car>()
                };

                _context.Dealers.Add(dealer);
                _context.SaveChanges();
                transaction.Commit();

                Console.WriteLine("Новый дилер добавлен");
            }
            catch (Exception)
            {
                transaction.Rollback();
                Console.WriteLine("Произошла ошибка");
            }
        }

        private void AddCar()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                Console.WriteLine("Введите марку автомобиля:");
                var make = Console.ReadLine();
                Console.WriteLine("Введите модель автомобиля:");
                var model = Console.ReadLine();
                Console.WriteLine("Введите год выпуска:");
                var year = int.Parse(Console.ReadLine()!);
                Console.WriteLine("Введите ID дилера:");
                var dealerId = int.Parse(Console.ReadLine()!);

                var car = new Car { Make = make, Model = model, Year = year, DealerId = dealerId };
                _context.Cars.Add(car);
                _context.SaveChanges();
                transaction.Commit(); 

                Console.WriteLine("Новый автомобиль добавлен");
            }
            catch (Exception)
            {
                transaction.Rollback();
                Console.WriteLine("Произошла ошибка");
            }
        }

        private void UpdateCar()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                Console.WriteLine("Введите ID автомобиля для обновления:");
                var id = int.Parse(Console.ReadLine()!);

                var carToUpdate = _context.Cars.FirstOrDefault(c => c.Id == id);
                if (carToUpdate != null)
                {
                    Console.WriteLine($"Текущая модель: {carToUpdate.Model}. Введите новую модель:");
                    var newModel = Console.ReadLine();
                    carToUpdate.Model = newModel;

                    _context.SaveChanges();
                    transaction.Commit(); 

                    Console.WriteLine("Автомобиль обновлен");
                }
                else
                {
                    Console.WriteLine("Автомобиль с таким ID не найден");
                }
            }
            catch (Exception)
            {
                transaction.Rollback();  
                Console.WriteLine("Произошла ошибка");
            }
        }

        private void DeleteCar()
        {
            using var transaction = _context.Database.BeginTransaction();
            try
            {
                Console.WriteLine("Введите ID автомобиля для удаления:");
                var id = int.Parse(Console.ReadLine()!);

                var carToDelete = _context.Cars.FirstOrDefault(c => c.Id == id);
                if (carToDelete != null)
                {
                    _context.Cars.Remove(carToDelete);
                    _context.SaveChanges();
                    transaction.Commit(); 

                    Console.WriteLine("Автомобиль удален");
                }
                else
                {
                    Console.WriteLine("Автомобиль с таким ID не найден");
                }
            }
            catch (Exception)
            {
                transaction.Rollback(); 
                Console.WriteLine("Произошла ошибка");
            }
        }

        private void DisplayCars()
        {
            var allCars = _context.Cars.ToList();
            Console.WriteLine("Список всех автомобилей:");
            foreach (var car in allCars)
            {
                Console.WriteLine($"{car.Make} {car.Model} ({car.Year})");
            }
        }
        
        
    }
}
