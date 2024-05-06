using ADHD_Web.Server.Models;
using Microsoft.EntityFrameworkCore;

namespace ADHD_Web.Server.Services;

public class AdhdDbContext(DbContextOptions<AdhdDbContext> options) : DbContext(options)
{
    public required DbSet<User> Users { get; init; }
    public required DbSet<Player> Players { get; init; }
    public required DbSet<LevelRecord> LevelRecords { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<Player>().HasKey(p => p.Id);
        modelBuilder.Entity<LevelRecord>().HasKey(lr => lr.Id);
    }
}