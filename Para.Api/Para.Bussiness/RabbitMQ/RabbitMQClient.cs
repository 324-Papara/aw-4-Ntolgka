using System.Text;
using Newtonsoft.Json;
using Para.Schema;
using RabbitMQ.Client;

namespace Para.Bussiness.RabbitMQ;

public class RabbitMQClient : IDisposable
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    public string QueueName { get; }

    public RabbitMQClient(string hostName, int port, string userName, string password, string queueName)
    {
        var factory = new ConnectionFactory()
        {
            HostName = hostName,
            Port = port,
            UserName = userName,
            Password = password
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();
        QueueName = queueName;

        _channel.QueueDeclare(queue: QueueName,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null);
    }

    public void PublishEmailMessage(EmailMessage emailMessage)
    {
        var message = JsonConvert.SerializeObject(emailMessage);
        var body = Encoding.UTF8.GetBytes(message);

        _channel.BasicPublish(
            exchange: "",
            routingKey: QueueName,
            basicProperties: null,
            body: body
        );
    }

    public IModel GetChannel() => _channel;

    public void Dispose()
    {
        _channel?.Dispose();
        _connection?.Dispose();
    }
}