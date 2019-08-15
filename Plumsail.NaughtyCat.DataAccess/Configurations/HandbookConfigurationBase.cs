using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plumsail.NaughtyCat.Common.Extensions;
using Plumsail.NaughtyCat.Common.Interfaces;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess.Configurations
{
    public abstract class HandbookConfigurationBase<TEntity, TEnum> 
        where TEntity : HandbookBase, new()
        where TEnum : struct, IConvertible
    {
        protected HandbookConfigurationBase()
        {
            var t = typeof(TEnum);
            if (!t.IsEnum)
            {
                throw new ArgumentException("Data type is not an enumeration", nameof(TEnum));
            }
        }

        protected void ApplySeeding(EntityTypeBuilder<TEntity> builder)
        {
            var data = new List<TEntity>();

            foreach (var enumValue in Enum.GetValues(typeof(TEnum)).OfType<TEnum>())
            {
                var d = enumValue.GetEnumDescription();
                var entity = new TEntity()
                {
                    Id = enumValue.ToInt32(CultureInfo.InvariantCulture),
                    Name = d
                };

                // Description is also set, if implemented
                if (typeof(TEntity).GetInterface(nameof(IHasDescription)) != null)
                {
                    var faceRef = (IHasDescription) entity;
                    faceRef.Description = d;
                }
                data.Add(entity);
            }

            builder.HasData(data);
        }
    }
}