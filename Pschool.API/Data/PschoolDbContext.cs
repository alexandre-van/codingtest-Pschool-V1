using Microsoft.EntityFrameworkCore;
using Pschool.API.Models.Entities;

public class PschoolDbContext : DbContext
{
    public PschoolDbContext(DbContextOptions<PschoolDbContext> options) : base(options)
    {
    }

    public DbSet<Parent> Parents { get; set; }
    public DbSet<Student> Students { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Parent>()
            .HasMany(e => e.Students)
            .WithOne(e => e.Parent)
            .HasForeignKey(e => e.ParentId)
            .IsRequired();
    }
}