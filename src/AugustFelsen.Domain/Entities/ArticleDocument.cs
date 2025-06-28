using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustFelsen.Domain.Entities;

public class ArticleDocument
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string FileName { get; set; } = string.Empty;
    public int ArticleId { get; set; }
    [ForeignKey("ArticleId")]
    public virtual Article Article { get; set; } = null!;
}