using System;
using System.Collections.Generic;
using System.Text;using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plumsail.NaughtyCat.Domain;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess.Configurations
{
    public class AppUserConfiguration: IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            
        }
    }
}
