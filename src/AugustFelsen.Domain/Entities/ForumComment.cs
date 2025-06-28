using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustFelsen.Domain.Entities;

public class ForumComment
{
    [Key]
    public int Id { get; set; }
    [MaxLength(2000)]
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int ForumPostId { get; set; }
    public string UserId { get; set; } = string.Empty;
    [ForeignKey("ForumPostId")]
    public virtual ForumPost ForumPost { get; set; } = null!;
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}