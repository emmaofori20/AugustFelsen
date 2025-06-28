using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustFelsen.Domain.Entities;

public class ProjectDocument
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string FileName { get; set; } = string.Empty;
    public int ProjectId { get; set; }
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
}