using Common.Vars;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Domain.Factories;

public sealed class DbContextFactory : IDbContextFactory<MediaManagerDatabaseContext>
{
    private readonly IAppInfo _appInfo;

    public DbContextFactory(IAppInfo appInfo)
    {
        _appInfo = appInfo ?? throw new ArgumentNullException(nameof(appInfo));
        SQLitePCL.Batteries.Init();
    }


    public MediaManagerDatabaseContext CreateDbContext()
    {
        var builder = new SqliteConnectionStringBuilder
        {
            DataSource = _appInfo.DatabasePath,
            // Password = // TODO: set password for database encryption 
        };
        
        var options = new DbContextOptionsBuilder<MediaManagerDatabaseContext>();
        options.UseSqlite(builder.ConnectionString);

        return new MediaManagerDatabaseContext(options.Options);
    }
}