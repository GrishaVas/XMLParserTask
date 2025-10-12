using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using XMLTask.Services.Models.XML;

namespace XMLTask.Services
{
    public class RabbitMQService : IDisposable
    {
        private readonly RabbitMQConfiguration _configuration;
        private readonly ConnectionFactory _connnectionFactory;
        private IConnection _connection;
        private IChannel _channel;
        private AsyncEventingBasicConsumer _consumer;

        public RabbitMQService(RabbitMQConfiguration configuration)
        {
            _configuration = configuration;
            _connnectionFactory = new ConnectionFactory { HostName = _configuration.Host };
        }

        public async Task Connect()
        {
            if ((_channel?.IsOpen ?? false))
            {
                throw new Exception("Channel is already connected.");
            }

            _connection = await _connnectionFactory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();

            await _channel.QueueDeclareAsync(_configuration.QueueName,
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            GC.ReRegisterForFinalize(this);
        }

        public async Task OnReceivingInstrumentStatus(Action<InstrumentStatus> action, Action<Exception> onError = null)
        {
            if (!(_channel?.IsOpen ?? false))
            {
                throw new Exception("Channel is not connected.");
            }

            _consumer = new AsyncEventingBasicConsumer(_channel);

            _consumer.ReceivedAsync += (model, e) =>
            {
                try
                {
                    if (e.RoutingKey == _configuration.QueueName)
                    {
                        var body = e.Body.ToArray();
                        var jsonInstrumentStatus = Encoding.UTF8.GetString(body);
                        var options = new JsonSerializerOptions() { AllowOutOfOrderMetadataProperties = true };
                        var instrumentStatus = JsonSerializer.Deserialize<InstrumentStatus>(jsonInstrumentStatus, options);

                        action?.Invoke(instrumentStatus);
                    }
                }
                catch (Exception ex)
                {
                    onError?.Invoke(ex);
                }

                return Task.CompletedTask;
            };

            await _channel.BasicConsumeAsync(_configuration.QueueName, autoAck: true, consumer: _consumer);
        }

        public async Task SendInstrumentStatus(InstrumentStatus instrumentStatus)
        {
            if (!(_channel?.IsOpen ?? false))
            {
                throw new Exception("Channel is not connected.");
            }

            var json = JsonSerializer.Serialize(instrumentStatus);
            var body = Encoding.UTF8.GetBytes(json);

            await _channel.BasicPublishAsync(exchange: string.Empty, routingKey: _configuration.QueueName, body: body);
        }

        public async Task Disconnect()
        {
            await _channel?.CloseAsync();
            await _connection?.CloseAsync();
        }

        public void Dispose()
        {
            _channel?.Dispose();
            _connection?.Dispose();
            GC.SuppressFinalize(this);
        }

        ~RabbitMQService()
        {
            _channel?.Dispose();
            _connection?.Dispose();
        }
    }
}
