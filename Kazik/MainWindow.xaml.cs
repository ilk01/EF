using System.IO;
using System.Windows;
using Kazik.Models;
using Kazik.Service;

namespace Kazik
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClearOldData()
        {
            StatusTextBlock.Text = string.Empty;
            NumbersTextBlock.Text = string.Empty;
            CasinoReportTextBlock.Text = string.Empty;
            GlobalProgressBar.Value = 0;

            NumbersTextBlock.Visibility = Visibility.Collapsed;
            CasinoReportTextBlock.Visibility = Visibility.Collapsed;
        }

        private async void GenerateNumbersButton_Click(object sender, RoutedEventArgs e)
        {
            ClearOldData();
            StatusTextBlock.Text = "Генерация чисел...";

            var progress = new Progress<int>(value =>
            {
                GlobalProgressBar.Value = value; 
            });

            await Task.Run(() => NumberGenerator.Generate(progress));

            StatusTextBlock.Text = "Числа сгенерированы!";
            string numbersDisplay = string.Join(", ", await File.ReadAllLinesAsync("numbers.txt"));
            NumbersTextBlock.Text = "Сгенерированные числа: " + numbersDisplay;

            NumbersTextBlock.Visibility = Visibility.Visible;
        }

        private async void FilterPrimesButton_Click(object sender, RoutedEventArgs e)
        {
            ClearOldData();
            StatusTextBlock.Text = "Фильтрация простых чисел...";

            var progress = new Progress<int>(value =>
            {
                GlobalProgressBar.Value = value;
            });

            await Task.Run(() => PrimeFilter.FilterNumber(progress));

            GlobalProgressBar.Value = 100;
            StatusTextBlock.Text = "Простые числа отфильтрованы!";

            var primes = await File.ReadAllLinesAsync("primes.txt");
            NumbersTextBlock.Text = "Простые числа: " + string.Join(", ", primes);

            NumbersTextBlock.Visibility = Visibility.Visible;
        }

        private async void FilterEndsWithButton_Click(object sender, RoutedEventArgs e)
        {
            ClearOldData();
            StatusTextBlock.Text = "Фильтрация чисел, заканчивающихся на 7...";

            await Task.Run(() => EndsWith7Filter.Filter());

            GlobalProgressBar.Value = 100;
            StatusTextBlock.Text = "Фильтрация завершена.";

            var endsWith7Numbers = await File.ReadAllLinesAsync("endsWith7.txt");
            NumbersTextBlock.Text = "Числа, заканчивающиеся на 7: " + string.Join(", ", endsWith7Numbers);

            NumbersTextBlock.Visibility = Visibility.Visible;
        }

        private async void GenerateReportButton_Click(object sender, RoutedEventArgs e)
        {
            ClearOldData();
            StatusTextBlock.Text = "Создание отчёта...";

            var progress = new Progress<int>(value =>
            {
                GlobalProgressBar.Value = value;
            });

            await Task.Run(() => ReportBuilder.Build(progress));

            GlobalProgressBar.Value = 100;
            StatusTextBlock.Text = "Отчёт создан!";

            var reportContent = await File.ReadAllTextAsync("report.txt");
            NumbersTextBlock.Text = "Отчёт:\n" + reportContent;

            NumbersTextBlock.Visibility = Visibility.Visible;
        }

        private async void StartCasinoButton_Click(object sender, RoutedEventArgs e)
        {
            ClearOldData();
            StatusTextBlock.Text = "Запуск казино...";

            var progress = new Progress<int>(value =>
            {
                GlobalProgressBar.Value = value;
            });

            var casinoService = new CasinoService();
            

            Random random = new Random();

            var betNumber = random.Next(1, 37);

            foreach (var player in casinoService.GetPlayers())
            {
                var betAmount = random.Next(1, player.Balance + 1);
                player.PlaceBet(betNumber, betAmount);
            }

            var report = await casinoService.RunCasino(progress);

            casinoService.SaveReportToFile(report);

            GlobalProgressBar.Value = 100;
            StatusTextBlock.Text = "Казино завершено!";

            CasinoReportTextBlock.Text = report;
            CasinoReportTextBlock.Visibility = Visibility.Visible;
        }

        

    }
}
