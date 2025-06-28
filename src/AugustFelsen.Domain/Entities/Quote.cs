using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustFelsen.Domain.Entities;

public class Quote
{
    [Key]
    public int Id { get; set; }
    public int SupplierProfileId { get; set; }
    public int RFQId { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    [ForeignKey("SupplierProfileId")]
    public virtual SupplierProfile SupplierProfile { get; set; } = null!;
    [ForeignKey("RFQId")]
    public virtual RFQ RFQ { get; set; } = null!;
}