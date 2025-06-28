using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AugustFelsen.Domain.Enums;

namespace AugustFelsen.Domain.Entities;

public class RFQ
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string? Description { get; set; }
    public RFQStatus Status { get; set; } = RFQStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<Quote> Quotes { get; set; } = new List<Quote>();
}