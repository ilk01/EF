using CodeFirst.Data.Contexts;
using CodeFirst.Data.Models;

namespace CodeFirst.Data.NavigationMenu;

public class SaleMenu
{
    private readonly ShowroomContext _context;

    public SaleMenu(ShowroomContext context)
    {
        _context = context;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("1. Добавить новую продажу");
            Console.WriteLine("2. Удалить продажу");
            Console.WriteLine("3. Обновить данные продажи");
            Console.WriteLine("4. Вывести все продажи");
            Console.WriteLine("5. Вернуться в главное меню");
            var choice = WriteRead(string.Empty);

            switch (choice)
            {
                case "1":
                    AddSale();
                    break;
                case "2":
                    DeleteSale();
                    break;
                case "3":
                    UpdateSale();
                    break;
                case "4":
                    ShowAllSales();
                    break;
                case "5":
                    return; 
            }
        }
        
    }

    private void AddSale()
    {
        var customerId = int.Parse(WriteRead("Введите ID клиента: "));
        var carId = int.Parse(WriteRead("Введите ID автомобиля: "));
        var employeeId = int.Parse(WriteRead("Введите ID сотрудника: "));
        var saleDate = DateTime.Parse(WriteRead("Введите дату продажи (dd.MM.yyyy): "));
        var price = decimal.Parse(WriteRead("Введите цену продажи: "));
        
        var sale = new Sale(carId, customerId, employeeId, saleDate, price);
        
        if (sale.Price < 0)
        {
            throw new ArgumentException("Цена отрицательна");
        }
        _context.Sales.Add(sale);
        _context.SaveChanges();
        ShowMenu();
    }


    private void DeleteSale()
    {
        var id = int.Parse(WriteRead("Введите ID продажи для удаления: "));
        var sale = _context.Sales.FirstOrDefault(s => s.Id == id);
        if (sale != null)
        {
            _context.Sales.Remove(sale);
            _context.SaveChanges();
            Console.WriteLine("Продажа удалена!");
        }
        else
        {
            Console.WriteLine("Продажа не найдена");
        }
        ShowMenu();
    }

    private void UpdateSale()
    {
        var id = int.Parse(WriteRead("Введите ID продажи для изменены: "));
        var sale = _context.Sales.FirstOrDefault(s => s.Id == id);
        if (sale != null)
        {
            sale.CustomerId = int.Parse(WriteRead("Введите новый ID клиента: "));
            sale.CarId = int.Parse(WriteRead("Введите новый ID автомобиля: "));
            sale.Price = decimal.Parse(WriteRead("Введите новую цену продажи: "));
            sale.SaleDate = DateTime.Parse(WriteRead("Введите новую дату продажи (dd.MM.yyyy): "));
            _context.SaveChanges();
        }
        else
        {
            Console.WriteLine("Продажа не найдена");
        }
        ShowMenu();
    }

    private void ShowAllSales()
    {
        var sales = _context.Sales.ToList();
        foreach (var sale in sales)
        {
            Console.WriteLine($"ID: {sale.Id}, ID клиента: {sale.CustomerId}, ID автомобиля: {sale.CarId}, Дата продажи: {sale.SaleDate}");
        }
        ShowMenu();
    }

    private string WriteRead(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? string.Empty;
    }
}