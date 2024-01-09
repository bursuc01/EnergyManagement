using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiteMonitor.BusinessLogicLayer.EnergyConsumptionBLL;

namespace SiteMonitor.BusinessLogicLayer.ConsumerDevice;

public class ConsumerDevice : IHostedService
{
        private IConnection? _connection;
        private IModel? _channel;
        private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly IServiceProvider _serviceProvider;
        
        public ConsumerDevice(
            IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        
        public Task StartAsync(CancellationToken cancellationToken)
        {
            Task.Run(() =>
            {
                const string url = "amqps://nutflmfw:1o6HpOTTwK1cVXxAmG1mdQblLc2_plz5@goose.rmq2.cloudamqp.com/nutflmfw";
                // create a connection and open a channel, dispose them when done
                var factory = new ConnectionFactory
                {
                    Uri = new Uri(url)
                };
        
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
        
                // ensure that the queue exists before we access it
                var queueName = "queue2";
                bool durable = false;
                bool exclusive = false;
                bool autoDelete = true;
        
                _channel.QueueDeclare(
                    queueName, 
                    durable, 
                    exclusive, 
                    autoDelete, 
                    null);
        
                var consumer = new EventingBasicConsumer(_channel);
            
                // add the message receive event
                consumer.Received += async (model, deliveryEventArgs) =>
                {
                    var body = deliveryEventArgs.Body.ToArray();
                    
                    // convert the message back from byte[] to a string
                    var message = int.Parse(Encoding.UTF8.GetString(body));
                    Console.WriteLine("** Received message: {0} by Consumer thread **", message);

                    // Creating a scope for my Service
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var scopedService = scope.ServiceProvider.GetRequiredService<IEnergyConsumptionService>();
                        await scopedService.DeleteMessagesWithDeviceIdAsync(message);
                    }
                    
                    _channel.BasicAck(deliveryEventArgs.DeliveryTag, false);
                };
        
                // start consuming
                _ = _channel.BasicConsume(
                    consumer, 
                    queueName);
                
                return Task.CompletedTask;
                
            }, cancellationToken);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            // Wait for the reset event and clean up when it triggers
            _resetEvent.WaitOne();
            _channel?.Close();
            _channel = null;
            _connection?.Close();
            _connection = null;
            return Task.CompletedTask;
        }

}