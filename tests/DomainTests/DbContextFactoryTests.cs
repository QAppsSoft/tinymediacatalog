using Domain.Factories;
using Microsoft.EntityFrameworkCore;
using TestsCommons.Helpers;

namespace DomainTests;

public class DbContextFactoryTests
{
    [Test]
    public void Database_should_be_created()
    {
        bool created;
        
        using var _ = MocksFixture.BuildAppInfo(out var appInfo);
        var dbcFactory = new DbContextFactory(appInfo);
        using var context = dbcFactory.CreateDbContext();
        
        try
        {
            context.Database.EnsureDeleted(); // Fix test fail if database file already present
            created = context.Database.EnsureCreated();
        }
        finally
        {
            context.Database.EnsureDeleted();
        }

        created.Should().BeTrue();
    }
    
    [Test]
    public void Correct_connection_string()
    {
        using var _ = MocksFixture.BuildAppInfo(out var appInfo);
        var dbcFactory = new DbContextFactory(appInfo);
        using var context = dbcFactory.CreateDbContext();

        var connectionString = context.Database.GetConnectionString();

        connectionString.Should().Contain(appInfo.DataBaseFileName);
    }
}