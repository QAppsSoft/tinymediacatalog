using Domain;
using Domain.Models;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestsCommons.Domain;

public sealed class DatabaseSeedDataFixture : IDisposable
{
    private readonly StorageFixture _storageFixture = new();
    public Func<MediaManagerDatabaseContext> MediaManagerDatabaseContextFactory { get; }
    private readonly string _databasePath;

    public DatabaseSeedDataFixture()
    {
        _databasePath = _storageFixture.GetTemporalFileName(".db");
        
        using var databaseContext = GetContext();

        databaseContext.Database.EnsureCreated();

        databaseContext.Persons.Add(new Person
        {
            Name = "Matt Schulze", Profile = "https://www.themoviedb.org/person/31841",
            Thumb = "https://image.tmdb.org/t/p/h632/wFOVJr3dxEs5kC2rtS5iV3XbU0C.jpg", UniqueIds = new List<UniqueId>
            {
                new() { Name = UniqueId.ValidNames.Tmdb, Id = "31841" },
            },
        });

        databaseContext.SaveChanges();

        MediaManagerDatabaseContextFactory = GetContext;
    }

    private MediaManagerDatabaseContext GetContext()
    {
        var builder = new SqliteConnectionStringBuilder { DataSource =  _databasePath};

        var options = new DbContextOptionsBuilder<MediaManagerDatabaseContext>();
        options.UseSqlite(builder.ConnectionString);

        return new MediaManagerDatabaseContext(options.Options);
    }

    public void Dispose()
    {
        _ = GetContext().Database.EnsureDeleted();
        _storageFixture.Dispose();
    }
}