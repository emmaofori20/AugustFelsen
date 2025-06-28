using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AugustFelsen.Domain.Entities;

public class SupplierProduct
{
    [Key]
    public int Id { get; set; }
    [Required]
    [MaxLength(200)]
    public string Name { get; set; } = string.Empty;
    public int SupplierProfileId { get; set; }
    [ForeignKey("SupplierProfileId")]
    public virtual SupplierProfile SupplierProfile { get; set; } = null!;
}