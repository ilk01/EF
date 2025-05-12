using System.IO;
using System.Net.Http;

namespace FileDownloaderApp.Download
{
    public class FileDownloader
    {
        private readonly string _logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "download_log.txt");
        private static readonly HttpClient Client = new();

        static FileDownloader()
        {
            Client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/91.0.4472.124 Safari/537.36");
            Client.DefaultRequestHeaders.Add("Accept", "application/pdf, text/html, application/xhtml+xml, application/xml;q=0.9, image/webp, */*;q=0.8");
            Client.DefaultRequestHeaders.Add("Connection", "keep-alive");
        }

        public async Task DownloadFile(string fileName, string url, Action<string> updateStatus, IProgress<int> progress)
        {
            updateStatus($"Поток {fileName} начал загрузку");

            Log($"[{DateTime.Now}] Поток {fileName} начал загрузку: {url}");

            try
            {
                var response = await Client.GetAsync(url);

                Console.WriteLine($"Ответ от сервера: {response.StatusCode}");

                if (response.IsSuccessStatusCode)
                {
                    Directory.CreateDirectory("downloads");

                    var filePath = Path.Combine("downloads", $"{fileName}.pdf");

                    var count = 1;
                    while (File.Exists(filePath) && IsFileLocked(filePath))
                    {
                        filePath = Path.Combine("downloads", $"{fileName}_{count}.pdf");
                        count++;
                    }

                    using (var fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                    {
                        var contentStream = await response.Content.ReadAsStreamAsync();
                        var buffer = new byte[8192];
                        int bytesRead;

                        var totalBytes = response.Content.Headers.ContentLength!.Value;
                        long bytesDownloaded = 0;

                        while ((bytesRead = await contentStream.ReadAsync(buffer, 0, buffer.Length)) > 0)
                        {
                            await fileStream.WriteAsync(buffer, 0, bytesRead);
                            bytesDownloaded += bytesRead;

                            var percentage = (int)((bytesDownloaded / (double)totalBytes) * 100);
                            progress.Report(percentage);
                        }
                    }

                    updateStatus($"Поток {fileName} завершил загрузку");
                    Log($"[{DateTime.Now}] Поток {fileName} завершил загрузку: {url}");
                }
                else
                {
                    updateStatus($"Ошибка при загрузке файла {fileName}");
                    Log($"[{DateTime.Now}] Ошибка при загрузке {fileName}: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                updateStatus($"Ошибка при загрузке файла {fileName}: {ex.Message}");
                Log($"[{DateTime.Now}] Ошибка при загрузке {fileName}: {ex.Message}");
            }
        }

        private void Log(string message)
        {
            try
            {
                File.AppendAllText(_logFilePath, message + Environment.NewLine);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка записи лога: " + ex.Message);
            }
        }

        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (new FileStream(filePath, FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                {
                    return false; 
                }
            }
            catch (IOException)
            {
                return true;
            }
        }
    }
}
