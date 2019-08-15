using Plumsail.NaughtyCat.DataAccess.Providers.Abstract;
using Plumsail.NaughtyCat.Domain.Models;

namespace Plumsail.NaughtyCat.DataAccess.Providers
{
    public class RabbitsProvider: GenericProviderBase<Rabbit>
    {
        public RabbitsProvider(NaughtyCatDbContext context):base(context)
        {
            
        }
    }
}
