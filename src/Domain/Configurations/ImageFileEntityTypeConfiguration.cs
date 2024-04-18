using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class ImageFileEntityTypeConfiguration : IEntityTypeConfiguration<ImageFile>
{
    public void Configure(EntityTypeBuilder<ImageFile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("ImageFiles");
    }
}