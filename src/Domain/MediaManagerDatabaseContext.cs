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
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MediaManagerDatabaseContext).Assembly);
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