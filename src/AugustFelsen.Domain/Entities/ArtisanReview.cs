using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustFelsen.Domain.Entities;

public class ArtisanReview
{
    [Key]
    public int Id { get; set; }
    public int ArtisanProfileId { get; set; }
    [MaxLength(2000)]
    public string? Content { get; set; }
    public int Rating { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey("ArtisanProfileId")]
    public virtual ArtisanProfile ArtisanProfile { get; set; } = null!;
}