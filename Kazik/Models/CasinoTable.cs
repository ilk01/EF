namespace Kazik.Models;

public class CasinoTable
{
    private readonly List<Player> _players = [];
    private readonly Roulette _roulette = new();  

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }

    public List<Player> GetPlayers()
    {
        return _players;
    }

    public Roulette GetRoulette()
    {
        return _roulette;
    }
}