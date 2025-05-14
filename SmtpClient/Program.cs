using SmtpClient.Models;
using SmtpClient.Services;

namespace SmtpClient;

static class Program
{
    static async Task Main(string[] args)
    {
        Console.WriteLine("Введите ваш адрес электронной почты:");
        var from = Console.ReadLine();

        Console.WriteLine("Введите адрес получателя:");
        var to = Console.ReadLine();

        Console.WriteLine("Введите тему письма:");
        var subject = Console.ReadLine();

        Console.WriteLine("Введите текст письма:");
        var body = Console.ReadLine();

        var message = new EmailMessage
        {
            From = from,
            To = to,
            Subject = subject,
            Body = body
        };

        var sender = new EmailSender();
        await sender.SendAsync(message);

        Console.WriteLine("Сообщение отправлено!");
    }
}