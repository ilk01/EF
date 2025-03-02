using CodeFirst.Data.Contexts;
using CodeFirst.Data.Models;

namespace CodeFirst.Data.NavigationMenu;

public class CustomerMenu
{
    private readonly ShowroomContext _context;

    public CustomerMenu(ShowroomContext context)
    {
        _context = context;
    }

    public void ShowMenu()
    {
        while (true)
        {
            Console.WriteLine("1. Добавить нового клиента");
            Console.WriteLine("2. Удалить клиента");
            Console.WriteLine("3. Обновить данные клиента");
            Console.WriteLine("4. Вывести всех клиентов");
            Console.WriteLine("5. Вернуться в главное меню");
            var choice = WriteRead(string.Empty);

            switch (choice)
            {
                case "1":
                    AddCustomer();
                    break;
                case "2":
                    DeleteCustomer();
                    break;
                case "3":
                    UpdateCustomer();
                    break;
                case "4":
                    ShowAllCustomers();
                    break;
                case "5":
                    return; 
            }
        }
    }


    private void AddCustomer()
    {
        var firstName = WriteRead("Введите имя клиента: ");
        var lastName = WriteRead("Введите фамилию клиента: ");
        var email = WriteRead("Введите email клиента: ");
        var phoneNumber = WriteRead("Введите номер телефона клиента: ");
        var customer = new Customer(firstName, lastName, email, phoneNumber);
        _context.Customers.Add(customer);
        _context.SaveChanges();
        Console.WriteLine("Клиент добавлен!");
        ShowMenu();
    }

    private void DeleteCustomer()
    {
        var id = int.Parse(WriteRead("Введите ID клиента для удаления: "));
        var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            _context.Customers.Remove(customer);
            _context.SaveChanges();
            Console.WriteLine("Клиент удалён!");
        }
        else
        {
            Console.WriteLine("Клиент не найден");
        }
        ShowMenu();
    }

    private void UpdateCustomer()
    {
        var id = int.Parse(WriteRead("Введите ID клиента для обновления: "));
        var customer = _context.Customers.FirstOrDefault(c => c.Id == id);
        if (customer != null)
        {
            customer.FirstName = WriteRead("Введите новое имя клиента: ");
            customer.LastName = WriteRead("Введите новое фамилию клиента: ");
            customer.Email = WriteRead("Введите новый email клиента: ");
            customer.PhoneNumber = WriteRead("Введите новый номер телефона клиента: ");
            _context.SaveChanges();
            Console.WriteLine("Клиент обновлен!");
        }
        else
        {
            Console.WriteLine("Клиент не найден");
        }
        ShowMenu();
    }

    private void ShowAllCustomers()
    {
        var customers = _context.Customers.ToList();
        foreach (var customer in customers)
        {
            Console.WriteLine($"ID: {customer.Id}, Имя: {customer.FirstName}, Фамилия: {customer.LastName}, Email: {customer.Email}, Телефон: {customer.PhoneNumber}");
        }
        ShowMenu();
    }

    private string WriteRead(string message)
    {
        Console.Write(message);
        return Console.ReadLine() ?? string.Empty;
    }
}
