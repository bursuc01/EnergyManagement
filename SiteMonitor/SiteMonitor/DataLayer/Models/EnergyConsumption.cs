using System.ComponentModel.DataAnnotations;

namespace SiteMonitor.DataLayer.Models;

public class EnergyConsumption
{
    [Key]
    public int Id { get; set; }
    
    public string Timestamp { get; set; }
    
    public int DeviceId { get; set; }
    
    public double MeasurementValue { get; set; }
}