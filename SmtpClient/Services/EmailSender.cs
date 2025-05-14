using System.Net;
using System.Net.Mail;
using System.Text.Json;
using SmtpClient.Models;

namespace SmtpClient.Services;

public class EmailSender
{
    private readonly SmtpSettings? _settings;

    public EmailSender()
    {
        var json = File.ReadAllText("appsettings.json");
        _settings = JsonSerializer.Deserialize<SmtpSettings>(json);
    }

    public async Task SendAsync(EmailMessage message)
    {
        using var client = new System.Net.Mail.SmtpClient(_settings?.Host, _settings!.Port);
        client.Credentials = new NetworkCredential(_settings.Username, _settings.Password);
        client.EnableSsl = _settings.UseSsl;

        var mail = new MailMessage(message.From!, message.To!, message.Subject, message.Body)
        {
            IsBodyHtml = _settings.IsHtml
        };

        if (!string.IsNullOrEmpty(message.AttachmentPath) && File.Exists(message.AttachmentPath))
        {
            mail.Attachments.Add(new Attachment(message.AttachmentPath));
        }

        Log("Подключение к SMTP-серверу...", ConsoleColor.Yellow);

        try
        {
            Log("Отправка сообщения", ConsoleColor.Yellow);
            await client.SendMailAsync(mail);
            Log("Сообщение успешно отправлено", ConsoleColor.Green);
        }
        catch (SmtpException ex)
        {
            Log($"Ошибка SMTP: {ex.Message}", ConsoleColor.Red);
        }
        catch (Exception ex)
        {
            Log($"Общая ошибка: {ex.Message}", ConsoleColor.Red);
        }
        finally
        {
            Console.ResetColor();
        }
    }

    private void Log(string message, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        Console.WriteLine(message);
        Console.ResetColor();
    }
    
}