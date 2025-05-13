namespace Kazik.Models;

public class Roulette
{
    private readonly Random _random = new();
    public int Spin()
    {
        return _random.Next(0, 37);
    }

}