using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class ActorMovieEntityTypeConfiguration : IEntityTypeConfiguration<ActorMovie>
{
    public void Configure(EntityTypeBuilder<ActorMovie> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.HasKey(e => new { e.PersonId, e.MovieContainerId });
    }
}