using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Web.Dto;

namespace Plumsail.NaughtyCat.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
        }


        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(LoginDto viewModel)
        {
            if (viewModel == null)
            {
                return BadRequest("Invalid client request");
            }

            var loginResult = new LoginResultDto()
            {
                Succeeded = false
            };


            var user = await _userManager.FindByEmailAsync(viewModel.Email);

            if (user == null)
            {
                return Ok(loginResult);
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, viewModel.Password, false);
            if (!result.Succeeded)
            {
                return Ok(loginResult);
            }

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

            return Ok(loginResult);
        }
    }
}