using CodeFirst.Data.Contexts;
using CodeFirst.Data.Models;

namespace CodeFirst.Data.NavigationMenu
{
    public class EmployeeMenu
    {
        private readonly ShowroomContext _context;

        public EmployeeMenu(ShowroomContext context)
        {
            _context = context;
        }
        
        public void ShowMenu()
        {
            while (true)
            {
                Console.WriteLine("1. Добавить нового сотрудника");
                Console.WriteLine("2. Удалить сотрудника");
                Console.WriteLine("3. Обновить данные сотрудника");
                Console.WriteLine("4. Вывести всех сотрудников");
                Console.WriteLine("5. Вернуться в главное меню");
                var choice = WriteRead(string.Empty);

                switch (choice)
                {
                    case "1":
                        AddEmployee();
                        break;
                    case "2":
                        DeleteEmployee();
                        break;
                    case "3":
                        UpdateEmployee();
                        break;
                    case "4":
                        ShowAllEmployees();
                        break;
                    case "5":
                        return; 
                }
            }
            
        }

        private void AddEmployee()
        {
            var firstName = WriteRead("Введите имя сотрудника: ");
            var lastName = WriteRead("Введите фамилию сотрудника: ");

            Console.WriteLine("Выберите должность:");
            foreach (var position in Enum.GetValues(typeof(Employee.Positions)))
            {
                Console.WriteLine(position); // Выводим название должности
            }

            var positionChoice = WriteRead("Введите название должности: ");
    
            if (Enum.IsDefined(typeof(Employee.Positions), positionChoice))
            {
                var position = (Employee.Positions)Enum.Parse(typeof(Employee.Positions), positionChoice);
        
                var employee = new Employee(firstName, lastName, position);
                _context.Employees.Add(employee);
                _context.SaveChanges();
                Console.WriteLine("Сотрудник добавлен!");
            }
            else
            {
                Console.WriteLine("Некорректное имя должности. Попробуйте снова.");
            }

            ShowMenu();
        }
        

        private void DeleteEmployee()
        {
            var id = int.Parse(WriteRead("Введите ID сотрудника для удаления: "));
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
                _context.SaveChanges();
                Console.WriteLine("Сотрудник удалён!");
            }
            else
            {
                Console.WriteLine("Сотрудник не найден");
            }
            ShowMenu();
        }

        private void UpdateEmployee()
        {
            var id = int.Parse(WriteRead("Введите ID сотрудника для обновления: "));
            var employee = _context.Employees.FirstOrDefault(e => e.Id == id);

            if (employee != null)
            {
                employee.FirstName = WriteRead("Введите новое имя сотрудника: ");
                employee.LastName = WriteRead("Введите новую фамилию сотрудника: ");

                Console.WriteLine("Выберите новую должность:");
        
                // Перебор и вывод всех значений enum
                foreach (Employee.Positions position in Enum.GetValues(typeof(Employee.Positions)))
                {
                    Console.WriteLine(position);  // Выводим название должности
                }

                var positionChoice = WriteRead("Введите название новой должности: ");
        
                // Пытаемся найти соответствующую должность из enum
                if (Enum.IsDefined(typeof(Employee.Positions), positionChoice))
                {
                    employee.Position = (Employee.Positions)Enum.Parse(typeof(Employee.Positions), positionChoice);
                    _context.SaveChanges();
                    Console.WriteLine("Данные обновлены!");
                }
                else
                {
                    Console.WriteLine("Неверная должность");
                }
            }
            else
            {
                Console.WriteLine("Сотрудник не найден");
            }

            ShowMenu();
        }
        

        private void ShowAllEmployees()
        {
            var employees = _context.Employees.ToList();
            foreach (var employee in employees)
            {
                Console.WriteLine($"ID: {employee.Id}, Имя: {employee.FirstName}, Фамилия: {employee.LastName}, Должность: {employee.Position}");
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
