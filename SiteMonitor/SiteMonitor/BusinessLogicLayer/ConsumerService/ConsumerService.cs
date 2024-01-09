using System.Text;
using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using SiteMonitor.BusinessLogicLayer.EnergyConsumptionBLL;
using SiteMonitor.BusinessLogicLayer.WebSocketService;
using SiteMonitor.DataLayer.DTO;

namespace SiteMonitor.BusinessLogicLayer.ConsumerService
{
    public class ConsumerService : IHostedService
    {
        private IConnection? _connection;
        private IModel? _channel;
        private ManualResetEvent _resetEvent = new ManualResetEvent(false);
        private readonly IServiceProvider _serviceProvider;
        private readonly IHubContext<SocketHub> _sockethub;
        
        public ConsumerService(
            IServiceProvider serviceProvider,
            IHubContext<SocketHub> sockethub)
        {
            _serviceProvider = serviceProvider;
            _sockethub = sockethub;
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
                var queueName = "queue1";
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
                    var message = JsonConvert
                        .DeserializeObject(Encoding.UTF8.GetString(body));
                    Console.WriteLine("** Received message: {0} by Consumer thread **", message);
                    
                    // ack the message, ie. confirm that we have processed it
                    // otherwise it will be requeued a bit later
                    var savedMessage = JsonConvert.DeserializeObject<EnergyConsumptionDTO>(Encoding.UTF8.GetString(body));
                    
                    // Creating a scope for my Service
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        var scopedService = scope.ServiceProvider.GetRequiredService<IEnergyConsumptionService>();
                        await scopedService.PostMessageAsync(savedMessage);
                    }
                    
                    _channel.BasicAck(deliveryEventArgs.DeliveryTag, false);

                    if (savedMessage != null)
                    {
                        var device = await GetDeviceWithId(savedMessage.DeviceId);
                    
                        if (device != null && device.MaximumHourlyEnergyConsumption < savedMessage.MeasurementValue)
                        {
                            Console.WriteLine("Sent notification to user");
                            await _sockethub.Clients.All.SendAsync("ReceiveMessage", "Exceeded maximum hourly energy consumption for device: " + device.Description);
                        }
                    }
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
        
        private static async Task<DeviceDTO?> GetDeviceWithId(int deviceId)
        {
            const string url = "http://172.19.0.11:5267/api/Device/";
            var maximumEnergy = new DeviceDTO();
            var httpClient = new HttpClient();
            
            try
            {
                var response = await httpClient.GetAsync(url + deviceId);
                var responseBody = await response.Content
                    .ReadAsStringAsync();
                
                Console.WriteLine(responseBody);
                
                maximumEnergy = JsonConvert.DeserializeObject<DeviceDTO>(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine("Http request error: " + ex.Message);
            }

            return maximumEnergy;
        }
        
    }
}