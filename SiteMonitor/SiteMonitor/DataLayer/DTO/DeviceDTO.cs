namespace SiteMonitor.DataLayer.DTO;

public class DeviceDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public float? MaximumHourlyEnergyConsumption { get; set; }
}