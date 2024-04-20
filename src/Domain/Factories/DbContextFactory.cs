using Common.Vars;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Domain.Factories;

public sealed class DbContextFactory(IAppInfo appInfo) : IDbContextFactory<MediaManagerDatabaseContext>
{
    private readonly IAppInfo _appInfo = appInfo ?? throw new ArgumentNullException(nameof(appInfo));

    public MediaManagerDatabaseContext CreateDbContext()
    {
        var builder = new SqliteConnectionStringBuilder
        {
            DataSource = _appInfo.DatabasePath,
            // Password = // TODO: set password for database encryption 
        };
        
        var options = new DbContextOptionsBuilder<MediaManagerDatabaseContext>();
        options.UseSqlite(builder.ConnectionString);
        SQLitePCL.Batteries.Init();

        return new MediaManagerDatabaseContext(options.Options);
    }
}