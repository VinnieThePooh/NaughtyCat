using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net.Mime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Plumsail.NaughtyCat.Web.Dto;

namespace Plumsail.NaughtyCat.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost, Route("login")]
        public IActionResult Login(LoginDto viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var loginResult = new LoginResultDto()
            {
                Succeeded = false
            };
            

            // todo: add repos or try asp.identity

            if (viewModel.Email == "johndoe@gmail.com" && viewModel.Password == "doepass")
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ncatsecretKey@567"));
                var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var jwt = new JwtSecurityToken(
                    issuer: "https://localhost:44388",
                    audience: "https://localhost:44388", 
                    claims: new List<Claim>(), 
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCredentials);
                
                loginResult.Succeeded = true;
                loginResult.Token = new JwtSecurityTokenHandler().WriteToken(jwt);
            }

            return Ok(loginResult);
        }
    }
}