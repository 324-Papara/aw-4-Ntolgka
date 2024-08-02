using System.Text;
using Newtonsoft.Json;
using Para.Bussiness.Notification;
using Para.Bussiness.RabbitMQ;
using Para.Schema;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;

namespace Para.Api.Service;

public class EmailProcessor
{
    private readonly RabbitMQClient _rabbitMqClient;
    private readonly INotificationService _notificationService;
    private readonly ILogger<EmailProcessor> _logger;
    private CancellationTokenSource _cts;

    public EmailProcessor(RabbitMQClient rabbitMqClient, INotificationService notificationService, ILogger<EmailProcessor> logger)
    {
        _rabbitMqClient = rabbitMqClient;
        _notificationService = notificationService;
        _logger = logger;
    }

    public async Task ProcessEmailQueue(CancellationToken cancellationToken)
    {
        var channel = _rabbitMqClient.GetChannel();

        var consumer = new EventingBasicConsumer(channel);
        consumer.Received += async (model, ea) =>
        {
            if (cancellationToken.IsCancellationRequested)
            {
                channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                return;
            }

            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var emailMessage = JsonConvert.DeserializeObject<EmailMessage>(message);

            if (emailMessage == null)
            {
                _logger.LogWarning("Received null email message");
                channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: false);
                return;
            }

            try
            {
                await _notificationService.SendEmailAsync(emailMessage.Subject, emailMessage.Email, emailMessage.Content);
                channel.BasicAck(ea.DeliveryTag, multiple: false); // Acknowledge the message
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error processing message");
                channel.BasicNack(ea.DeliveryTag, multiple: false, requeue: true); // Requeue the message
            }
        };

        channel.BasicConsume(queue: _rabbitMqClient.QueueName,
            autoAck: false, // Manual acknowledgment
            consumer: consumer);

        try
        {
            await Task.Delay(Timeout.Infinite, cancellationToken);
        }
        catch (TaskCanceledException)
        {
            _logger.LogInformation("Email processing has been canceled.");
        }
        finally
        {
            channel.Close();
            _rabbitMqClient.Dispose();
        }
    }
}




