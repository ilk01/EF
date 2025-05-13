using System.IO;
using System.Text;
using Kazik.Models;

namespace Kazik.Service
{
    public class CasinoService
    {
        private readonly CasinoTable _casinoTable = new();

        public List<Player> GetPlayers()
        {
            return _casinoTable.GetPlayers();
        }

        public async Task<string> RunCasino(IProgress<int> progress)
        {
            GeneratePlayers();

            var report = new StringBuilder("Начинаем игру в казино...\n");
            var reportLock = new object();
            var rounds = 5;
            var players = _casinoTable.GetPlayers();

            for (var round = 1; round <= rounds; round++)
            {
                var roundReport = new StringBuilder($" Раунд {round} \n");
                var roundTasks = new List<Task>();

                foreach (var player in players)
                {
                    var task = Task.Run(() =>
                    {
                        if (player.Balance > 0 && player.BetAmount > 0 && player.BetNumber > 0)
                        {
                            var winningNumber = _casinoTable.GetRoulette().Spin();
                            Thread.Sleep(500);

                            var result = new StringBuilder();
                            result.AppendLine($"Вращаем рулетку... Число: {winningNumber}");

                            lock (player)
                            {
                                if (player.BetAmount > player.Balance)
                                {
                                    result.AppendLine($"Игрок {player.Id} не может поставить {player.BetAmount} — недостаточно средств.");
                                }
                                else if (player.BetNumber == winningNumber)
                                {
                                    player.Balance += player.BetAmount;
                                    result.AppendLine($"Игрок {player.Id} выиграл! Ставка {player.BetAmount} на {player.BetNumber}. Баланс: {player.Balance}");
                                }
                                else
                                {
                                    player.Balance -= player.BetAmount;
                                    result.AppendLine($"Игрок {player.Id} проиграл. Ставка {player.BetAmount} на {player.BetNumber}. Баланс: {player.Balance}");
                                }

                                if (player.Balance == 0)
                                {
                                    roundReport.AppendLine($"Игрок {player.Id} выходит (баланс 0).");
                                    player.Balance = 1000;
                                    player.PlaceBet(new Random().Next(1, 37), new Random().Next(1, 100)); 
                                    roundReport.AppendLine($"Новый игрок {player.Id} садится за стол (баланс: 1000).");
                                }
                            }

                            lock (reportLock)
                            {
                                roundReport.AppendLine(result.ToString());
                            }

                            progress.Report((int)((round * players.Count) / (float)(rounds * players.Count) * 100));
                        }
                    });

                    roundTasks.Add(task);
                }

                await Task.WhenAll(roundTasks);

                lock (reportLock)
                {
                    report.AppendLine(roundReport.ToString());
                }

                players = _casinoTable.GetPlayers().Where(p => p.Balance > 0).ToList();
            }
            var totalBalance = players.Sum(p => p.Balance);
            report.AppendLine($"\n Общий итог ");
            report.AppendLine($"Общий баланс всех игроков: {totalBalance}.");

            progress.Report(100);
            return report.ToString();
        }

        private void GeneratePlayers()
        {
            var random = new Random();
            var totalPlayers = random.Next(20, 40);

            for (var i = 0; i < totalPlayers; i++)
            {
                var startBalance = random.Next(500, 1200);
                var player = new Player(i + 1, startBalance);

                var betAmount = random.Next(77, 333);
                var betNumber = random.Next(1, 37);

                player.PlaceBet(betNumber, betAmount);
                _casinoTable.AddPlayer(player);
            }
        }

        public void SaveReportToFile(string reportContent)
        {
            var filePath = "casino_report.txt";
            try
            {
                File.WriteAllText(filePath, reportContent);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка сохранения отчёта: " + ex.Message);
            }
        }
    }
}
