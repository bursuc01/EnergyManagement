namespace ConsoleApp1.Producer;

using System;

public class Message
{
    public DateTime Timestamp { get; set; }

    public string MeasurementValue { get; set; }

    public int DeviceId { get; set; }

    public Message(
        DateTime timeStamp,
        string measurementValue,
        int deviceId
        )
    {
        Timestamp = timeStamp;
        MeasurementValue = measurementValue;
        DeviceId = deviceId;
    }
}
