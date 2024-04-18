using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class OtherFileEntityTypeConfiguration : IEntityTypeConfiguration<OtherFile>
{
    public void Configure(EntityTypeBuilder<OtherFile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("OtherFiles");
    }
}