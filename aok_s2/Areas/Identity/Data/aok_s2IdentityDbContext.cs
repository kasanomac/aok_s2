using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using aok_s2.Models;

namespace aok_s2.Areas.Identity.Data;

public class aok_s2IdentityDbContext : IdentityDbContext<IdentityUser>
{
    public aok_s2IdentityDbContext(DbContextOptions<aok_s2IdentityDbContext> options)
        : base(options)
    {
    }
    public DbSet<Major> Majors { get; set; } = default!;
    public DbSet<Class> Classes { get; set; } = default!;
    public DbSet<ClassMajor> ClassMajors { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        // ClassMajor（中間テーブル）
        builder.Entity<ClassMajor>()
            .HasKey(cm => new { cm.ClassId, cm.MajorId });

        builder.Entity<ClassMajor>()
            .HasOne(cm => cm.Class)
            .WithMany(c => c.ClassMajors)
            .HasForeignKey(cm => cm.ClassId);

        builder.Entity<ClassMajor>()
            .HasOne(cm => cm.Major)
            .WithMany(m => m.ClassMajors)
            .HasForeignKey(cm => cm.MajorId);
    }
}
