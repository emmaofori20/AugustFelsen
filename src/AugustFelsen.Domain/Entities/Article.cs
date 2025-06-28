using System.ComponentModel.DataAnnotations;

namespace AugustFelsen.Domain.Entities;

public class Article
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string? Content { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<ArticleDocument> Documents { get; set; } = new List<ArticleDocument>();
}