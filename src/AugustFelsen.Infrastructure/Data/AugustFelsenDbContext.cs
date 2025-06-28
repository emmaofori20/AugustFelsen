using AugustFelsen.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AugustFelsen.Infrastructure.Data;

public class AugustFelsenDbContext : IdentityDbContext<User, UserRole, string>
{
    public AugustFelsenDbContext(DbContextOptions<AugustFelsenDbContext> options) : base(options) {}

    public DbSet<Project> Projects { get; set; }
    public DbSet<Bid> Bids { get; set; }
    public DbSet<SupplierProfile> SupplierProfiles { get; set; }
    public DbSet<RFQ> RFQs { get; set; }
    public DbSet<Quote> Quotes { get; set; }
    public DbSet<ForumPost> ForumPosts { get; set; }
    public DbSet<ForumComment> ForumComments { get; set; }
    public DbSet<Article> Articles { get; set; }
    public DbSet<ArticleDocument> ArticleDocuments { get; set; }
    public DbSet<ProjectDocument> ProjectDocuments { get; set; }
    public DbSet<SupplierProduct> SupplierProducts { get; set; }
    public DbSet<ArtisanProfile> ArtisanProfiles { get; set; }
    public DbSet<ArtisanReview> ArtisanReviews { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Project relationships
        modelBuilder.Entity<Project>()
            .HasOne(p => p.Client)
            .WithMany(u => u.Projects)
            .HasForeignKey(p => p.ClientId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Project>()
            .HasOne(p => p.AssignedProfessional)
            .WithMany()
            .HasForeignKey(p => p.AssignedProfessionalId)
            .OnDelete(DeleteBehavior.Restrict);
        // Bid relationships
        modelBuilder.Entity<Bid>()
            .HasOne(b => b.Project)
            .WithMany(p => p.Bids)
            .HasForeignKey(b => b.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Bid>()
            .HasOne(b => b.User)
            .WithMany(u => u.Bids)
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        // Forum relationships
        modelBuilder.Entity<ForumPost>()
            .HasOne(fp => fp.User)
            .WithMany(u => u.ForumPosts)
            .HasForeignKey(fp => fp.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<ForumComment>()
            .HasOne(fc => fc.ForumPost)
            .WithMany(fp => fp.Comments)
            .HasForeignKey(fc => fc.ForumPostId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<ForumComment>()
            .HasOne(fc => fc.User)
            .WithMany(u => u.ForumComments)
            .HasForeignKey(fc => fc.UserId)
            .OnDelete(DeleteBehavior.Cascade);
        // Article relationships
        modelBuilder.Entity<ArticleDocument>()
            .HasOne(ad => ad.Article)
            .WithMany(a => a.Documents)
            .HasForeignKey(ad => ad.ArticleId)
            .OnDelete(DeleteBehavior.Cascade);
        // ProjectDocument relationships
        modelBuilder.Entity<ProjectDocument>()
            .HasOne(pd => pd.Project)
            .WithMany(p => p.Documents)
            .HasForeignKey(pd => pd.ProjectId)
            .OnDelete(DeleteBehavior.Cascade);
        // Supplier relationships
        modelBuilder.Entity<SupplierProduct>()
            .HasOne(sp => sp.SupplierProfile)
            .WithMany(s => s.Products)
            .HasForeignKey(sp => sp.SupplierProfileId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Quote>()
            .HasOne(q => q.SupplierProfile)
            .WithMany(s => s.Quotes)
            .HasForeignKey(q => q.SupplierProfileId)
            .OnDelete(DeleteBehavior.Cascade);
        modelBuilder.Entity<Quote>()
            .HasOne(q => q.RFQ)
            .WithMany(r => r.Quotes)
            .HasForeignKey(q => q.RFQId)
            .OnDelete(DeleteBehavior.Cascade);
        // Artisan relationships
        modelBuilder.Entity<ArtisanReview>()
            .HasOne(ar => ar.ArtisanProfile)
            .WithMany(ap => ap.Reviews)
            .HasForeignKey(ar => ar.ArtisanProfileId)
            .OnDelete(DeleteBehavior.Cascade);
    }
} 