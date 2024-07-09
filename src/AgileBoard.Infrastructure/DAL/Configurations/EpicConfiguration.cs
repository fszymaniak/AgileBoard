using AgileBoard.Core.Entities;
using AgileBoard.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileBoard.Infrastructure.DAL.Configurations;

internal sealed class EpicConfiguration : IEntityTypeConfiguration<Epic>
{
    public void Configure(EntityTypeBuilder<Epic> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
            .HasConversion(x => x.Value, x => new EpicId(x));
        builder.Property(x => x.Name)
            .HasConversion(x => x.Value, x => new Name(x));
        builder.Property(x => x.CreatedDate)
            .HasConversion(x => x.Value, x => new Date(x));

        builder
            .HasDiscriminator<string>("Type")
            .HasValue<DraftEpic>(nameof(DraftEpic))
            .HasValue<FinalEpic>(nameof(FinalEpic));
    }
}