using System.Net;
using System.Net.Sockets;
using System.Text;


namespace Serverr
{
    public class Server
    {
        private TcpListener? _server;
        private bool _isRunning;

        public void Start()
        {
            _server = new TcpListener(IPAddress.Any, 2003);
            _server.Start();
            _isRunning = true;

            Console.WriteLine("Сервер запущен!");

            while (_isRunning)
            {
                try
                {
                    var client = _server.AcceptTcpClient();
                    Thread clientThread = new Thread((HandleClient!));
                    clientThread.Start(client);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                }
            }
        }

        private void HandleClient(object obj)
        {
            var client = (TcpClient)obj;
            var stream = client.GetStream();
            var buffer = new byte[1024];

            try
            {
                var byteCount = stream.Read(buffer, 0, buffer.Length);
                var message = Encoding.UTF8.GetString(buffer, 0, byteCount);  
                Console.WriteLine($"Получено сообщение: {message}");

                var response = ProcessMessage(message);

                var responseBytes = Encoding.UTF8.GetBytes(response);  
                stream.Write(responseBytes, 0, responseBytes.Length);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка! при обработке клиента: {ex.Message}");
            }
            finally
            {
                client.Close();
            }
        }

        private string ProcessMessage(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
            {
                return "Ошибка пустое сообщение";
            }

            if (message.ToLower().Contains("мотивацию надо"))
            {
                return "ПОДНЯТЬЬЬЬЬЬ!";
            }

            if (message.ToLower().Contains("привет"))
            {
                return "Привет, Сударь!";
            }

            if (message.ToLower().Contains("пока"))
            {
                return "До скорой встречи мой господин!";
            }

            return $"Господина слово: {message}";
        }
    }
}
