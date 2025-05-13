namespace Kazik.Models;

public class Player
{
    public int Id { get; }
    public int Balance { get; set; }

    public int BetNumber { get; private set; }
    public int BetAmount { get; private set; }

    public Player(int id, int balance)
    {
        Id = id;
        Balance = balance;
    }

    public void PlaceBet(int number, int amount)
    {
        if (amount > 0 && amount <= Balance) 
        {
            BetAmount = amount;
            BetNumber = (number >= 1 && number <= 36) ? number : 0;  
        }
        else
        {
            BetAmount = 0;
            BetNumber = 0;
        }
    }

}