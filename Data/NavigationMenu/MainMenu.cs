using CodeFirst.Data.Contexts;

namespace CodeFirst.Data.NavigationMenu
{
    public class Menu
    {
        private readonly QueryMenu _queryMenu;
        private readonly CustomerMenu _customerMenu;
        private readonly CarMenu _carMenu;
        private readonly SaleMenu _saleMenu;
        private readonly EmployeeMenu _employeeMenu;
        private readonly ServiceHistoryMenu _serviceHistoryMenu; 

        public Menu(ShowroomContext context)
        {
            _queryMenu = new QueryMenu(context);
            _customerMenu = new CustomerMenu(context);
            _carMenu = new CarMenu(context);
            _saleMenu = new SaleMenu(context);
            _employeeMenu = new EmployeeMenu(context);
            _serviceHistoryMenu = new ServiceHistoryMenu(context); 
        }

        public void ShowMenu()
        {
            Console.WriteLine("1. Работа с клиентами");
            Console.WriteLine("2. Работа с автомобилями");
            Console.WriteLine("3. Работа с продажами");
            Console.WriteLine("4. Работа с сотрудниками");
            Console.WriteLine("5. История обслуживания"); 
            Console.WriteLine("6. Запросы");
            Console.WriteLine("7. Выход");
            var choice = WriteRead(string.Empty);

            switch (choice)
            {
                case "1":
                    _customerMenu.ShowMenu();
                    break;
                case "2":
                    _carMenu.ShowMenu();
                    break;
                case "3":
                    _saleMenu.ShowMenu();
                    break;
                case "4":
                    _employeeMenu.ShowMenu();
                    break;
                case "5":
                    _serviceHistoryMenu.ShowMenu(); 
                    break;
                case "6":
                    _queryMenu.ShowMenu();
                    break;
                case "7":
                    break;
            }
        }

        private string WriteRead(string message)
        {
            Console.Write(message);
            return Console.ReadLine() ?? string.Empty;
        }
    }
}
