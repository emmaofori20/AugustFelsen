using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AugustFelsen.Domain.Entities;

public class UserRole : IdentityRole<string>
{
    [MaxLength(200)]
    public string? Description { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}