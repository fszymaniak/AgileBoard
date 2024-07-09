using AgileBoard.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AgileBoard.Infrastructure.DAL.Configurations;

public class DraftEpicConfiguration : IEntityTypeConfiguration<DraftEpic>
{
    public void Configure(EntityTypeBuilder<DraftEpic> builder)
    {
    }
}