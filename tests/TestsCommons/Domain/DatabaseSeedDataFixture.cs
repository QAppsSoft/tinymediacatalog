using System.Diagnostics;
using Domain;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace TestsCommons.Domain;

public sealed class DatabaseSeedDataFixture : IDisposable
{
    private readonly StorageFixture _storageFixture = new();
    public Func<MediaManagerDatabaseContext> MediaManagerDatabaseContextFactory { get; }
    private readonly DbContextOptions<MediaManagerDatabaseContext> _contextOptions;

    public DatabaseSeedDataFixture()
    {
        var databasePath = _storageFixture.GetTemporalFileName(".db");
        
        var builder = new SqliteConnectionStringBuilder { DataSource =  databasePath};

        var options = new DbContextOptionsBuilder<MediaManagerDatabaseContext>();
        options.UseSqlite(builder.ConnectionString);
        options.EnableSensitiveDataLogging();
        options.LogTo(s => Debug.WriteLine(s));
        
        _contextOptions = options.Options;
        
        using var databaseContext = GetContext();

        databaseContext.Database.EnsureCreated();

        MediaManagerDatabaseContextFactory = GetContext;
    }

    private MediaManagerDatabaseContext GetContext() => new(_contextOptions);

    public void Dispose()
    {
        _ = GetContext().Database.EnsureDeleted();
        _storageFixture.Dispose();
    }
}