using CodeFirst.Data.Contexts;

namespace CodeFirst.Data.NavigationMenu
{
    public class QueryMenu
    {
        private readonly ShowroomContext _context;

        public QueryMenu(ShowroomContext context)
        {
            _context = context;
        }
        
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Найти все автомобили, купленные клиентом");
                Console.WriteLine("2. Вывести список продаж за определенный период");
                Console.WriteLine("3. Подсчитать количество продаж каждого менеджера");
                Console.WriteLine("4. Вернуться в главное меню");
                Console.Write("Выберите опцию: ");
                var choice = WriteRead(string.Empty);

                switch (choice)
                {
                    case "1":
                        FindCarsByCustomer();
                        break;
                    case "2":
                        ShowSalesByDateRange();
                        break;
                    case "3":
                        CountSalesByManager();
                        break;
                    case "4":
                        return; 

                }
            }
            
        }

        private void FindCarsByCustomer()
        {
            var customerId = int.Parse(WriteRead("Введите ID клиента: "));
            var cars = _context.Sales
                .Where(s => s.CustomerId == customerId)
                .Select(s => s.Car)
                .ToList();

            if (cars.Count != 0)
            {
                foreach (var car in cars)
                {
                    Console.WriteLine($"Марка: {car.Brand}, Модель: {car.Model}, Год: {car.Year}, Цена: {car.Price}");
                }
            }
            else
            {
                Console.WriteLine("У клиента нет покупок");
            }

            ShowMenu();
        }

        private void ShowSalesByDateRange()
        {
            var startDate = DateTime.Parse(WriteRead("Введите начальную дату (dd.MM.yyyy): "));
            var endDate = DateTime.Parse(WriteRead("Введите конечную дату (dd.MM.yyyy): "));

            var sales = _context.Sales
                .Where(s => s.SaleDate >= startDate && s.SaleDate <= endDate)
                .ToList();

            var any = sales.Count != 0;

            if (any)
            {
                foreach (var sale in sales)
                {
                    Console.WriteLine($"ID продажи: {sale.Id}, Клиент: {sale.CustomerId}, Автомобиль: {sale.CarId}, Дата: {sale.SaleDate}, Цена: {sale.Price}");
                }
            }
            else
            {
                Console.WriteLine("Продажи за указанный период не найдено");
            }

            ShowMenu();
        }

        private void CountSalesByManager()
        {
            var salesByManager = _context.Sales
                .GroupBy(s => s.EmployeeId)
                .Select(g => new
                {
                    ManagerId = g.Key,
                    SalesCount = g.Count()
                })
                .ToList();

            foreach (var item in salesByManager)
            {
                Console.WriteLine($"ID менеджера: {item.ManagerId}, Количество продаж: {item.SalesCount}");
            }

            ShowMenu();
        }

        private string WriteRead(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
