using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;

namespace WebApplication1.services.email;

public class EmailService
{
    
    private readonly EmailSettings _emailSettings;
    private readonly ILogger<EmailService> _logger;
    
    public EmailService(IOptions<EmailSettings> emailSettings, ILogger<EmailService> logger)
    {
        _emailSettings = emailSettings.Value;
        _logger = logger;
    }
    public async Task SendEmailAsync(string toEmail, string subject, string body, bool isHtml = true)
    {
        try
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.SenderEmail));
            message.To.Add(new MailboxAddress("", toEmail));
            message.Subject = subject;

            var bodyBuilder = new BodyBuilder();
            if (isHtml)
            {
                bodyBuilder.HtmlBody = body;
            }
            else
            {
                bodyBuilder.TextBody = body;
            }
            message.Body = bodyBuilder.ToMessageBody();

            using var client = new SmtpClient();
            
            // Conectar ao servidor SMTP
            await client.ConnectAsync(_emailSettings.SmtpServer, _emailSettings.Port,
                _emailSettings.EnableSsl ? SecureSocketOptions.StartTls : SecureSocketOptions.None);
            
            // Autenticar
            await client.AuthenticateAsync(_emailSettings.Username, _emailSettings.Password);
            
            // Enviar email
            await client.SendAsync(message);
            
            await client.DisconnectAsync(true);
            
            _logger.LogInformation("Email enviado com sucesso para {ToEmail}", toEmail);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro ao enviar email para {ToEmail}", toEmail);
            throw;
        }
    }
}