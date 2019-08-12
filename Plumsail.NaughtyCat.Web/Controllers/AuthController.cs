using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Plumsail.NaughtyCat.Web.ViewModels;

namespace Plumsail.NaughtyCat.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login([FromBody] LoginViewModel viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Invalid client request");
            }

            // get somewhere
            object someUser = null;

            if (viewModel.Email == "johndoe@gmail.com" && viewModel.Password == "doepass")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ncatsecretKey@567"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(
                    issuer: "https://localhost:65039",
                    audience: "https://localhost:65039", 
                    claims: new List<Claim>(), 
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials);

                var tokenString = new JwtSecurityTokenHandler().WriteToken(jwt);
                return Ok(new {Token = tokenString});
            }

            return Unauthorized();
        }
    }
}