using Domain.Models.Movie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public class MovieContainerEntityTypeConfiguration:IEntityTypeConfiguration<MovieContainer>
{
    public void Configure(EntityTypeBuilder<MovieContainer> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("Movies");
        builder.HasIndex(mc => mc.BasePath);
    }
}