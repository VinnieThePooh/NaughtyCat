using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess.Configurations
{
    public class RabbitPriorityConfiguration : IEntityTypeConfiguration<RabbitPriority>
    {
        public void Configure(EntityTypeBuilder<RabbitPriority> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Rabbits).WithOne(x => x.Priority).HasForeignKey(x => x.IdRabbitPriority)
                .IsRequired(false);
        }
    }
}