using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SiteUser.DataLayer.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string? Name { get; set; }
    [Required]
    public string? Password { get; set; }
    public bool IsAdmin { get; set; }
}