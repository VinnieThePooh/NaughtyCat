using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.Models.Jwt;

namespace Plumsail.NaughtyCat.Web.Controllers
{
    //todo: refactor
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;

        public AccountController(SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager, IAuthService authService)
        {
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
        }


        [HttpPost, Route("login")]
        public async Task<IActionResult> Login(JwtTokenRequest request)
        {
            if (request == null)
            {
                return BadRequest("Invalid client request");
            }

            var rResult = await _authService.AuthenticateRequest(request);
            return Ok(rResult);
        }
    }
}