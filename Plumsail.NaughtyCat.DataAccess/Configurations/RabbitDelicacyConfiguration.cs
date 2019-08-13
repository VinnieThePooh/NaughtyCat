using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plumsail.NaughtyCat.Common.Enums;
using Plumsail.NaughtyCat.Common.Helpers;
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

            var data = new List<RabbitDelicacy>();

            //todo: refactor. this is reusable part
            foreach (var delicacy in Enum.GetValues(typeof(DelicacyEnum)).OfType<DelicacyEnum>())
            {
                var d = delicacy.GetEnumDescription();
                data.Add(new RabbitDelicacy()
                {
                    Id = (int)delicacy,
                    Name = d,
                    Description = d
                });
            }
            builder.HasData(data);
        }
    }
}