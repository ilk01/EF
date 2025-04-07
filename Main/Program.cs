using async_await.User;

namespace async_await.Main;

class Program
{
    static async Task Main(string[] args)
    {
        await UserInterface.ShowMenuAsync();
    }
}