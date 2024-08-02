namespace Para.Bussiness.Notification;

public interface INotificationService
{
    Task SendEmailAsync(string subject, string email, string content);
}   