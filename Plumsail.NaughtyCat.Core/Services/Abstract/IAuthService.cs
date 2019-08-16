using System.Threading.Tasks;
using Plumsail.NaughtyCat.Domain.Models.Jwt;

namespace Plumsail.NaughtyCat.Core.Services.Abstract
{
    public interface IAuthService
    {
        Task<JwtRequestResult> IsAuthenticated(JwtTokenRequest request);
    }
}
