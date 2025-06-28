using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AugustFelsen.Domain.Enums;

namespace AugustFelsen.Domain.Entities;

public class Bid
{
    [Key]
    public int Id { get; set; }
    public int ProjectId { get; set; }
    public string UserId { get; set; } = string.Empty;
    public decimal Amount { get; set; }
    public BidStatus Status { get; set; } = BidStatus.Pending;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey("ProjectId")]
    public virtual Project Project { get; set; } = null!;
    [ForeignKey("UserId")]
    public virtual User User { get; set; } = null!;
}