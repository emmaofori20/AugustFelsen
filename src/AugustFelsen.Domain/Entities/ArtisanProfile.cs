using System.ComponentModel.DataAnnotations;

namespace AugustFelsen.Domain.Entities;

public class ArtisanProfile
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Bio { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<ArtisanReview> Reviews { get; set; } = new List<ArtisanReview>();
}