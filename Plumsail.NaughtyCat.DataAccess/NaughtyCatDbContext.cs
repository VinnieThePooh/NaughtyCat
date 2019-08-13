using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Plumsail.NaughtyCat.DataAccess.Configurations;
using Plumsail.NaughtyCat.Domain;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess
{
    public class NaughtyCatDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public NaughtyCatDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            // todo: get workaround about table names eq. ToTable method
            builder.ApplyConfiguration(new AppRoleConfiguration());
            builder.ApplyConfiguration(new AppUserConfiguration());
            builder.ApplyConfiguration(new RabbitConfiguration());
            builder.ApplyConfiguration(new RabbitDelicacyConfiguration());
            builder.ApplyConfiguration(new RabbitPriorityConfiguration());
        }

        public DbSet<Rabbit> Rabbits { get; protected set; }

    }
}