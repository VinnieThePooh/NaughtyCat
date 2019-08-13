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
    public class RabbitPriorityConfiguration : IEntityTypeConfiguration<RabbitPriority>
    {
        public void Configure(EntityTypeBuilder<RabbitPriority> builder)
        {
            builder.HasIndex(x => x.Name).IsUnique();
            builder.HasMany(x => x.Rabbits).WithOne(x => x.Priority).HasForeignKey(x => x.IdRabbitPriority)
                .IsRequired(false);

            var data = new List<RabbitPriority>();

            //todo: refactor. this is reusable part
            foreach (var priority in Enum.GetValues(typeof(PriorityEnum)).OfType<PriorityEnum>())
            {
                var d = priority.GetEnumDescription();
                data.Add(new RabbitPriority()
                {
                    Id = (int)priority,
                    Name = d,
                    Description = d
                });
            }
            builder.HasData(data);
        }
    }
}