using Domain;
using Microsoft.EntityFrameworkCore;

namespace TestsCommons.Domain;

public sealed class DbContextFactoryFixture : IDbContextFactory<MediaManagerDatabaseContext>, IDisposable
{
    private readonly DatabaseSeedDataFixture _fixture = new();
    
    public MediaManagerDatabaseContext CreateDbContext()
    {
        return _fixture.MediaManagerDatabaseContextFactory();
    }

    public void Dispose()
    {
        _fixture.Dispose();
    }
}