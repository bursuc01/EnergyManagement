using System.Text;
using AutoMapper;
using RabbitMQ.Client;
using SiteDevice.DataLayer.DTO;
using SiteDevice.DataLayer.Models;
using SiteDevice.DataLayer.Repository.DeviceRepository;

namespace SiteDevice.BusinessLogicLayer.DeviceBLL;

public class DeviceService : IDeviceService
{
    private readonly IDeviceRepository _deviceRepository;
    private readonly IMapper _mapper;

    public DeviceService(
        IDeviceRepository deviceRepository,
        IMapper mapper)
    {
        _deviceRepository = deviceRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<DeviceGetDTO>> GetDevicesAsync()
    {
        var devices = await _deviceRepository.GetDevicesAsync();
        return _mapper.Map<IEnumerable<DeviceGetDTO>>(devices);
    }

    public async Task<DeviceDTO> GetDeviceAsync(int id)
    {
        var device = await _deviceRepository.GetDeviceAsync(id);
        return _mapper.Map<DeviceDTO>(device);
    }

    public async Task PutDeviceAsync(DeviceGetDTO device)
    {
        var actualDevice = _mapper.Map<Device>(device);
        await _deviceRepository.PutDeviceAsync(actualDevice);
    }

    public async Task PostDeviceAsync(DeviceDTO device)
    {
        var actualDevice = _mapper.Map<Device>(device);
        await _deviceRepository.PostDeviceAsync(actualDevice);
    }

    public async Task DeleteDeviceAsync(int id)
    {
        await _deviceRepository.DeleteDeviceAsync(id);
        SendDeviceIdToConsumer(id);
    }

    private static void SendDeviceIdToConsumer(int deviceId)
    {
    // CloudAMQP URL in format amqp://user:pass@hostName:port/vhost
    const string url = "amqps://nutflmfw:1o6HpOTTwK1cVXxAmG1mdQblLc2_plz5@goose.rmq2.cloudamqp.com/nutflmfw";

    // Create a ConnectionFactory and set the Uri to the CloudAMQP url
    // the connectionfactory is stateless and can safetly be a static resource in your app
    var factory = new ConnectionFactory
        {
            Uri = new Uri(url)
        };
    // create a connection and open a channel, dispose them when done
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
    // ensure that the queue exists before we publish to it
    var queueName = "queue2";
    bool durable = false;
    bool exclusive = false;
    bool autoDelete = true;

    channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);
    
    // the data put on the queue must be a byte array
    var data = Encoding.UTF8.GetBytes(deviceId.ToString());
    // publish to the "default exchange", with the queue name as the routing key
    var exchangeName = "";
    var routingKey = queueName;
    channel.BasicPublish(exchangeName, routingKey, null, data);
    }
    
}