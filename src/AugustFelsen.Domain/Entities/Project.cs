using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AugustFelsen.Domain.Enums;

namespace AugustFelsen.Domain.Entities;

public class Project
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Title { get; set; } = string.Empty;
    [MaxLength(2000)]
    public string? Description { get; set; }
    public ProjectStatus Status { get; set; } = ProjectStatus.Open;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public string ClientId { get; set; } = string.Empty;
    public string? AssignedProfessionalId { get; set; }
    [ForeignKey("ClientId")]
    public virtual User Client { get; set; } = null!;
    [ForeignKey("AssignedProfessionalId")]
    public virtual User? AssignedProfessional { get; set; }
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
    public virtual ICollection<ProjectDocument> Documents { get; set; } = new List<ProjectDocument>();
}