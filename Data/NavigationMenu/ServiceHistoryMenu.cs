using CodeFirst.Data.Contexts;
using CodeFirst.Data.Models;

namespace CodeFirst.Data.NavigationMenu
{
    public class ServiceHistoryMenu
    {
        private readonly ShowroomContext _context;

        public ServiceHistoryMenu(ShowroomContext context)
        {
            _context = context;
        }

        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Добавить запись об обслуживании");
                Console.WriteLine("2. Посмотреть историю обслуживания для автомобиля");
                Console.WriteLine("3. Вернуться в главное меню");
                var choice = WriteRead(string.Empty);

                switch (choice)
                {
                    case "1":
                        AddServiceHistory();
                        break;
                    case "2":
                        ViewServiceHistory();
                        break;
                    case "3":
                        return; 
                }
            }
        }

        private void AddServiceHistory()
        {
            var carId = int.Parse(WriteRead("Введите ID автомобиля: "));
            var serviceDate = DateTime.Parse(WriteRead("Введите дату обслуживания (dd.MM.yyyy): "));
            var description = WriteRead("Введите описание обслуживания: ");

            var serviceHistory = new ServiceHistory(carId, serviceDate, description);
            _context.ServiceHistories.Add(serviceHistory);
            _context.SaveChanges();

            Console.WriteLine("Запись об обслуживании добавлена!");
            ShowMenu();
        }

        private void ViewServiceHistory()
        {
            var carId = int.Parse(WriteRead("Введите ID автомобиля: "));
            var history = _context.ServiceHistories
                .Where(s => s.CarId == carId)
                .ToList();

            if (history.Any())
            {
                foreach (var record in history)
                {
                    Console.WriteLine($"Дата: {record.ServiceDate}, Описание: {record.Description}");
                }
            }
            else
            {
                Console.WriteLine("У автомобиля нет записей об обслуживании");
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
