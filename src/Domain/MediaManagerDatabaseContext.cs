using Domain.Configurations;
using Domain.Extensions;
using Domain.Models;
using Domain.Models.Interfaces;
using Domain.Models.Movie;
using Microsoft.EntityFrameworkCore;

namespace Domain;

public class MediaManagerDatabaseContext : DbContext
{
    public MediaManagerDatabaseContext()
    {
    }

    // TODO: Fix IL2026
    public MediaManagerDatabaseContext(DbContextOptions<MediaManagerDatabaseContext> options) : base(options)
    {
    }
    
    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        HandleAdded();
        HandleModified();

        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    public DbSet<MovieControl> MovieControls { get; set; } = null!;
    
    public DbSet<MovieCollection> MovieCollections { get; set; } = null!;
    public DbSet<MovieContainer> Movies { get; set; } = null!;
    public DbSet<Person> Persons { get; set; } = null!;
    public DbSet<Genre> Genres { get; set; } = null!;
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfiguration(new ActorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AudioFileEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ImageFileEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MovieControlEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MultimediaFileEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new NfoFileEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new OtherFileEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new SubtitleFileEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VideoFileEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MovieContainerEntityTypeConfiguration());
        
        base.OnModelCreating(modelBuilder);
    }
    
    private void HandleModified()
    {
        var modified = ChangeTracker.Entries<ITrackedEntity>().Where(e => e.IsModified());
        foreach (var entry in modified)
        {
            entry.Property(x => x.Updated).CurrentValue = DateTime.Now;
            entry.Property(x => x.Updated).IsModified = true;
            entry.Property(x => x.Created).CurrentValue = entry.Property(x => x.Created).OriginalValue;
            entry.Property(x => x.Created).IsModified = false;
        }
    }

    private void HandleAdded()
    {
        var added = ChangeTracker.Entries<ITrackedEntity>().Where(e => e.State == EntityState.Added);
        foreach (var entry in added)
        {
            entry.Property(x => x.Created).CurrentValue = DateTime.Now;
            entry.Property(x => x.Created).IsModified = true;
            entry.Property(x => x.Updated).CurrentValue = DateTime.Now;
            entry.Property(x => x.Updated).IsModified = true;
        }
    }
}