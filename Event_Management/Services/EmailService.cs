using Event_Management.Services;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;

    public EmailService(IConfiguration config)
    {
        _config = config;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string body)
    {
        var senderEmail = _config["EmailSettings:SenderEmail"];
        var senderPassword = _config["EmailSettings:Password"];

        var email = new MimeMessage();
        email.From.Add(new MailboxAddress("Event Management", senderEmail));
        email.To.Add(new MailboxAddress("", toEmail));
        email.Subject = subject;
        email.Body = new TextPart("plain") { Text = body };

        using var smtp = new MailKit.Net.Smtp.SmtpClient(); // Explicitly use MailKit's SmtpClient
        await smtp.ConnectAsync(_config["EmailSettings:SmtpServer"], int.Parse(_config["EmailSettings:Port"]), SecureSocketOptions.SslOnConnect);
        await smtp.AuthenticateAsync(senderEmail, senderPassword);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
