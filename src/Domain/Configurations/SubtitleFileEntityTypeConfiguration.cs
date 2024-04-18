using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class SubtitleFileEntityTypeConfiguration : IEntityTypeConfiguration<SubtitleFile>
{
    public void Configure(EntityTypeBuilder<SubtitleFile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("SubtitlesFiles");
    }
}