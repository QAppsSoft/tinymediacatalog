using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class MovieControlEntityTypeConfiguration : IEntityTypeConfiguration<MovieControl>
{
    public void Configure(EntityTypeBuilder<MovieControl> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("MovieControls");
    }
}