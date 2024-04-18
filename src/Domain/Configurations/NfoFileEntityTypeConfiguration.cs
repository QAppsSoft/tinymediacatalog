using Domain.Models.Multimedia;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Domain.Configurations;

public sealed class NfoFileEntityTypeConfiguration : IEntityTypeConfiguration<NfoFile>
{
    public void Configure(EntityTypeBuilder<NfoFile> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("NfoFiles");
    }
}