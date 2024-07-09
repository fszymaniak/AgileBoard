using AgileBoard.Core.Entities;
using AgileBoard.Core.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileBoard.Infrastructure.DAL.Configurations;

internal sealed class FinalEpicConfiguration : IEntityTypeConfiguration<FinalEpic>
{
    public void Configure(EntityTypeBuilder<FinalEpic> builder)
    {
        builder.Property(x => x.Status)
            .HasConversion(x => x.Value, x => new Status(x));
        builder.Property(x => x.Description)
            .HasConversion(x => x.Value, x => new Description(x));
        builder.Property(x => x.AcceptanceCriteria)
            .HasConversion(x => x.Value, x => new AcceptanceCriteria(x));
    }
}