namespace SiteMonitor.DataLayer.DTO;

public class EnergyConsumptionDTO
{
    public string Timestamp { get; set; }
    
    public int DeviceId { get; set; }
    
    public double MeasurementValue { get; set; }
    
    public EnergyConsumptionDTO(
        string timeStamp,
        int deviceId,
        double measurementValue)
    {
        Timestamp = timeStamp;
        DeviceId = deviceId;
        MeasurementValue = measurementValue;
    }
}