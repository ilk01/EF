using System.Net.Sockets;
using System.Text;
using System.Windows;
using System.Windows.Input;

namespace WpfTcpClientServer
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MessageTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter) return;
            var message = MessageTextBox.Text;
            if (string.IsNullOrWhiteSpace(message))
            {
                ResponseTextBox.Text = "Сообщение не может быть пустым!";
            }
            else
            {
                _ = SendMessageToServerAsync(message);
                MessageTextBox.Clear(); 
            }
        }
        private async void SendButton_Click(object sender, RoutedEventArgs e)
        {
            var message = MessageTextBox.Text;
            if (string.IsNullOrWhiteSpace(message))
            {
                ResponseTextBox.Text = "Сообщение не может быть пустым!";
            }
            else
            {
                await SendMessageToServerAsync(message);
                MessageTextBox.Clear();
            }
        }

        private async Task SendMessageToServerAsync(string message)
        {
            try
            {
                using TcpClient client = new TcpClient("127.0.0.1", 2003);
                NetworkStream stream = client.GetStream();
                byte[] data = Encoding.UTF8.GetBytes(message);
                await stream.WriteAsync(data, 0, data.Length);

                byte[] buffer = new byte[1024];
                var bytesRead = await stream.ReadAsync(buffer, 0, buffer.Length);
                string response = Encoding.UTF8.GetString(buffer, 0, bytesRead); 

                ResponseTextBox.Text = response;
            }
            catch (Exception ex)
            {
                ResponseTextBox.Text = "Ошибка: " + ex.Message;
            }
        }

    }
}
