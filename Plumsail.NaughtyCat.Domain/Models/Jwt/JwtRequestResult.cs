using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Domain.Models.Jwt
{
    public class JwtRequestResult
    {
        public bool Succeeded { get; set; }

        public string Token { get; set; }

        public UserDataDto UserData { get; set; } = new UserDataDto();
    }
}
