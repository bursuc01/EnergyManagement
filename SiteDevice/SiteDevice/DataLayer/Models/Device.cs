using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteDevice.DataLayer.Models;

public class Device
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Description { get; set; }
    [Required]
    public string? Address { get; set; }
    [Required]
    public float? MaximumHourlyEnergyConsumption { get; set; }
    public int? UserId { get; set; } 
    public User? User { get; set; }

}