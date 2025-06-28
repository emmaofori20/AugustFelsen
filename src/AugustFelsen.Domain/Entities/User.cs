using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using AugustFelsen.Domain.Enums;

namespace AugustFelsen.Domain.Entities;

public class User : IdentityUser<string>
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;
    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;
    [MaxLength(500)]
    public string? Bio { get; set; }
    [MaxLength(200)]
    public string? Company { get; set; }
    [MaxLength(200)]
    public string? Position { get; set; }
    [MaxLength(500)]
    public string? Address { get; set; }
    public UserType UserType { get; set; } = UserType.Client;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public virtual ICollection<Project> Projects { get; set; } = new List<Project>();
    public virtual ICollection<Bid> Bids { get; set; } = new List<Bid>();
    public virtual ICollection<ForumPost> ForumPosts { get; set; } = new List<ForumPost>();
    public virtual ICollection<ForumComment> ForumComments { get; set; } = new List<ForumComment>();
    public virtual ICollection<ArtisanReview> ArtisanReviews { get; set; } = new List<ArtisanReview>();
}