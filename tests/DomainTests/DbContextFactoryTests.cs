using Common.Vars;
using Domain.Factories;
using Microsoft.EntityFrameworkCore;

namespace DomainTests;

public class DbContextFactoryTests
{
    [Test]
    public void Database_should_be_created()
    {
        bool created;
        var dbcFactory = new DbContextFactory();
        using var context = dbcFactory.CreateDbContext();
        
        try
        {
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
        var dbcFactory = new DbContextFactory();
        using var context = dbcFactory.CreateDbContext();

        var connectionString = context.Database.GetConnectionString();

        connectionString.Should().Contain(PathProvider.DataBaseFileName);
    }
}