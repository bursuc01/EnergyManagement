namespace SiteDevice.DataLayer.DTO;

public class DeviceGetDTO
{
    public int Id { get; set; }
    public string? Description { get; set; }
    public string? Address { get; set; }
    public float? MaximumHourlyEnergyConsumption { get; set; }
    public int? UserId { get; set; }
}