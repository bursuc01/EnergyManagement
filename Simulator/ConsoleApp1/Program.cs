using System.Text;
using System.Timers;
using ConsoleApp1.Producer;
using Newtonsoft.Json;
using RabbitMQ.Client;

namespace ConsoleApp1;

internal abstract class Program
{
    // CloudAMQP URL in format amqp://user:pass@hostName:port/vhost
    private const string Url = "amqps://nutflmfw:1o6HpOTTwK1cVXxAmG1mdQblLc2_plz5@goose.rmq2.cloudamqp.com/nutflmfw";
    private static StreamReader _reader = new StreamReader("/home/ianis/RiderProjects/Simulator/ConsoleApp1/sensor.csv");
    
    private static void Main()
    {
        var aTimer = new System.Timers.Timer();
        aTimer.Elapsed += SendMessageToQueue;
        aTimer.Interval = 5 * 1000;
        aTimer.Enabled = true;
        
        Console.WriteLine("Press \'q\' to quit the sample.");
        while(Console.Read() != 'q');
        
        _reader.Close();
    }

    private static void SendMessageToQueue(object? state, ElapsedEventArgs args)    
    {
        var factory = new ConnectionFactory
        {
            Uri = new Uri(Url)
        };
        // create a connection and open a channel, dispose them when done
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();
        // ensure that the queue exists before we publish to it
        var queueName = "queue1";
        bool durable = false;
        bool exclusive = false;
        bool autoDelete = true;

        channel.QueueDeclare(queueName, durable, exclusive, autoDelete, null);
        
        var mess = CreateMessageObj();
        // the data put on the queue must be a byte array
        var message = JsonConvert.SerializeObject(mess);
        var data = Encoding.UTF8.GetBytes(message);
        // publish to the "default exchange", with the queue name as the routing key
        var exchangeName = "";
        var routingKey = queueName;
        channel.BasicPublish(exchangeName, routingKey, null, data);
        
        
        Console.WriteLine("Sent to queue " + data);
    }

    private static Message CreateMessageObj()
    {
        var file = new StreamReader("/home/ianis/RiderProjects/Simulator/ConsoleApp1/config.txt");
        var deviceId = file.ReadLine();
        var message = new Message(
            DateTime.Now,
            ReadFromFile(),
            int.Parse(deviceId)
        );
        Console.WriteLine("Created obj" + JsonConvert.SerializeObject(message));
        return message;
    }
    private static string? ReadFromFile()
    {
        var csvLine = "";

        try
        { 
            // Read the first line from the CSV file
            csvLine= _reader.ReadLine();
            Console.WriteLine("Read from file: " + csvLine);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error reading CSV file: {ex.Message}");
        }

        return csvLine;
    }
}