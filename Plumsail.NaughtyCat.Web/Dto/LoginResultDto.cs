using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plumsail.NaughtyCat.Web.Dto
{
    public class LoginResultDto
    {
        public bool Succeeded { get; set; }

        public string Token { get; set; }

        public UserDataDto UserData { get; set; } = new UserDataDto();
    }
}
