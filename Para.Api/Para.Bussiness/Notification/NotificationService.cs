using System.Net;
using System.Net.Mail;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Para.Schema;

namespace Para.Bussiness.Notification;

public class NotificationService : INotificationService
{
    private readonly IOptions<SmtpSettings> _smtpSettings;
    private readonly ILogger<NotificationService> _logger;

    public NotificationService(IOptions<SmtpSettings> smtpSettings, ILogger<NotificationService> logger)
    {
        _smtpSettings = smtpSettings;
        _logger = logger;
    }

    public async Task SendEmailAsync(string subject, string email, string content)
    {
        var smtpSettings = _smtpSettings.Value;
        using var smtpClient = new SmtpClient(smtpSettings.SmtpHost, smtpSettings.SmtpPort)
        {
            Credentials = new NetworkCredential(smtpSettings.SmtpUser, smtpSettings.SmtpPass),
            EnableSsl = true
        };

        var mailMessage = new MailMessage
        {
            From = new MailAddress(smtpSettings.FromEmail),
            Subject = subject,
            Body = content,
            IsBodyHtml = true,
        };

        mailMessage.To.Add(email);

        try
        {
            await smtpClient.SendMailAsync(mailMessage);
            _logger.LogInformation("Email sent successfully to {Email}", email);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error sending email to {Email}", email);
            throw;
        }
    }
}
