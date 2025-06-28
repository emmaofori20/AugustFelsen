using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustFelsen.Domain.Entities;

public class ForumPost
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
    public virtual ICollection<ForumComment> Comments { get; set; } = new List<ForumComment>();
}