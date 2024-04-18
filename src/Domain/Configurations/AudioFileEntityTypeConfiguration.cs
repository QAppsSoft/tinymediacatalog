using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class AudioFileEntityTypeConfiguration : IEntityTypeConfiguration<AudioFile>
{
    public void Configure(EntityTypeBuilder<AudioFile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("AudioFiles");
    }
}