using System.ComponentModel.DataAnnotations;

namespace AugustFelsen.Domain.Entities;

public class SupplierProfile
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<SupplierProduct> Products { get; set; } = new List<SupplierProduct>();
    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}