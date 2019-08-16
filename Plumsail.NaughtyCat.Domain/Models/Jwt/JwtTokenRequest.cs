using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace Plumsail.NaughtyCat.Domain.Models.Jwt
{
    public class JwtTokenRequest
    {
        [Required]
        [JsonProperty("email")]
        public string Email { get; set; }


        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
