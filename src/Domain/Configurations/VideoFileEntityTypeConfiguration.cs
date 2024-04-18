using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class VideoFileEntityTypeConfiguration : IEntityTypeConfiguration<VideoFile>
{
    public void Configure(EntityTypeBuilder<VideoFile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("VideoFiles");
    }
}