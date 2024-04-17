using Common.Vars;
using Microsoft.EntityFrameworkCore;

namespace Domain.Factories;

public sealed class DbContextFactory : IDbContextFactory<MediaManagerDatabaseContext>
{
    public MediaManagerDatabaseContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<MediaManagerDatabaseContext>();
        options.UseSqlite(AppStrings.ConnectionString);

        return new MediaManagerDatabaseContext(options.Options);
    }
}