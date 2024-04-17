﻿using Common.Vars;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;

namespace Domain.Factories;

public sealed class DbContextFactory : IDbContextFactory<MediaManagerDatabaseContext>
{
    public MediaManagerDatabaseContext CreateDbContext()
    {
        var builder = new SqliteConnectionStringBuilder
        {
            DataSource = PathProvider.DatabasePath,
            //Password = // TODO: set password for database encryption 
        };
        
        var options = new DbContextOptionsBuilder<MediaManagerDatabaseContext>();
        options.UseSqlite(builder.ConnectionString);
        SQLitePCL.Batteries.Init();

        return new MediaManagerDatabaseContext(options.Options);
    }
}