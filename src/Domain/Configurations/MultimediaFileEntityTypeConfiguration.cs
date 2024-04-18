using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class MultimediaFileEntityTypeConfiguration : IEntityTypeConfiguration<MultimediaFile>
{
    public void Configure(EntityTypeBuilder<MultimediaFile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.UseTpcMappingStrategy();
    }
}