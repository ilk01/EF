using CodeFirst.Data.Contexts;
using CodeFirst.Data.NavigationMenu;

namespace CodeFirst
{
    public class Program
    {
        public static void Main()
        {
            var context = new ShowroomContext();

            var menu = new Menu(context);

            menu.ShowMenu();
        }
    }
}

