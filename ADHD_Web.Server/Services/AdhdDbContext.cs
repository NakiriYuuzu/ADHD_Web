using ADHD_Web.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ADHD_Web.Server.Services;

public class AdhdDbContext(DbContextOptions<AdhdDbContext> options) : DbContext(options)
{
    public required DbSet<Players> Players { get; set; }
    public required DbSet<Levels> Levels { get; set; }
    public required DbSet<PlayerLevel> PlayerLevels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // Player 实体配置
        modelBuilder.Entity<Players>(entity =>
        {
            entity.HasKey(e => e.PlayerId);
            entity.Property(e => e.PlayerId).ValueGeneratedOnAdd();
            entity.Property(e => e.Name).IsRequired();
            entity.Property(e => e.Age).IsRequired();
            entity.Property(e => e.Gender).IsRequired();
        });
    
        // Level 实体配置
        modelBuilder.Entity<Levels>(entity =>
        {
            entity.HasKey(e => e.LevelId);
            entity.Property(e => e.LevelName).IsRequired();
            entity.Property(e => e.Difficulty).IsRequired();
        });
    
        // PlayerLevel 实体配置
        modelBuilder.Entity<PlayerLevel>(entity =>
        {
            entity.HasKey(e => new { e.PlayerId, e.LevelId });
    
            // entity.HasOne(d => d.Player)
            //     .WithMany(p => p.PlayerLevels)
            //     .HasForeignKey(d => d.PlayerId);
            //
            // entity.HasOne(d => d.Level)
            //     .WithMany(l => l.PlayerLevels)
            //     .HasForeignKey(d => d.LevelId);
    
            entity.Property(e => e.IsCompleted).IsRequired();
            entity.Property(e => e.TimeSpent).IsRequired();
        });
    }
}