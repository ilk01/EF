using System;
using System.Threading.Tasks;
using System.Windows;
using FileDownloaderApp.Download;

namespace FileDownloaderApp
{
    public partial class MainWindow : Window
    {
        private FileDownloader _fileDownloader;
        private string[] _urls;

        public MainWindow()
        {
            InitializeComponent();
            _fileDownloader = new FileDownloader();
        }

        private void OnDownloadButtonClick(object sender, RoutedEventArgs e)
        {
            _urls = UrlInput.Text.Split([','], StringSplitOptions.RemoveEmptyEntries);

            if (_urls.Length == 0)
            {
                MessageBox.Show("Пожалуйста, введите хотя бы один URL.");
                return;
            }

            ProgressBar.Value = 0;
            ProgressBar.Maximum = 100;
            for (var i = 0; i < _urls.Length; i++)
            {
                var index = i;
                var progress = new Progress<int>(value =>
                {
                    Dispatcher.Invoke(() => ProgressBar.Value = value);
                });

                RunDownloadAsync(index, progress);
            }
        }

        private async void RunDownloadAsync(int index, IProgress<int> progress)
        {
            var fileName = $"Поток {index + 1}";
            await _fileDownloader.DownloadFile(fileName, _urls[index], UpdateStatus, progress);
        }

        private void UpdateStatus(string status)
        {
            Dispatcher.Invoke(() =>
            {
                StatusListBox.Items.Add(status);

                if (StatusListBox.Items.Count == _urls.Length * 2) 
                {
                    CompletionMessage.Text = "Все файлы загружены!";
                }
            });
        }
    }
}
