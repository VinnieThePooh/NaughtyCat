using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess.Configurations
{
    public class RabbitDelicacyConfiguration : IEntityTypeConfiguration<RabbitDelicacy>
    {
        public void Configure(EntityTypeBuilder<RabbitDelicacy> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Rabbits).WithOne(x => x.Delicacy).HasForeignKey(x => x.IdRabbitDelicacy)
                .IsRequired(false);
        }
    }
}