using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess.Configurations
{
    public class RabbitConfiguration: IEntityTypeConfiguration<Rabbit>
    {
        public void Configure(EntityTypeBuilder<Rabbit> builder)
        {
            
        }
    }
}
