using Domain.Models.Movie;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class ActorEntityTypeConfiguration : IEntityTypeConfiguration<Actor>
{
    public void Configure(EntityTypeBuilder<Actor> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);
        builder.ToTable("Actors");
        builder.HasKey(e => new { e.PersonId, e.MovieContainerId });
    }
}