namespace ConsoleApp8
{
    public class NavigationMenu
    {
        private readonly CarService _carService;

        public NavigationMenu(string? connectionString)
        {
            _carService = new CarService(connectionString);
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Добавить автомобиль");
                Console.WriteLine("2. Обновить цену автомобиля");
                Console.WriteLine("3. Удалить автомобиль");
                Console.WriteLine("4. Показать все автомобили");
                Console.WriteLine("5. Выход");

                var choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddCar();
                        break;
                    case "2":
                        UpdateCarPrice();
                        break;
                    case "3":
                        DeleteCar();
                        break;
                    case "4":
                        ShowAllCars();
                        break;
                    case "5":
                        return;
                    default:
                        Console.WriteLine("Неверный выбор");
                        break;
                }
            }
        }

        private void AddCar()
        {
            var car = new Car
            {
                Brand = WriteRead("Введите марку автомобиля: "),
                Model = WriteRead("Введите модель автомобиля: "),
                Year = int.Parse(WriteRead("Введите год автомобиля: ") ?? string.Empty),
                Price = decimal.Parse(WriteRead("Введите цену автомобиля: ") ?? string.Empty)
            };
            _carService.AddCar(car);
        }

        private void UpdateCarPrice()
        {
            var id = int.Parse(WriteRead("Введите ID автомобиля: ") ?? string.Empty);
            var newPrice = decimal.Parse(WriteRead("Введите новую цену автомобиля: ") ?? string.Empty);
            _carService.UpdateCarPrice(id, newPrice);
        }

        private void DeleteCar()
        {
            var id = int.Parse(WriteRead("Введите ID автомобиля для удаления: ") ?? string.Empty);
            _carService.DeleteCar(id);
        }
        

        private void ShowAllCars()
        {
          _carService.GetAllCars(); 
        } 

        private string? WriteRead(string message)
        {
            Console.Write(message);
            return Console.ReadLine();
        }
    }
}
