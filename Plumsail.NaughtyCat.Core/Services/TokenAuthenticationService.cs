using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Plumsail.NaughtyCat.Core.Services.Abstract;
using Plumsail.NaughtyCat.Domain.Models;
using Plumsail.NaughtyCat.Domain.Models.Jwt;
using Plumsail.NaughtyCat.Domain.WebDto;

namespace Plumsail.NaughtyCat.Core.Services
{
    public class TokenAuthenticationService: IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly JwtTokenModel _tokenModel;

        public TokenAuthenticationService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IOptions<JwtTokenModel> tokenModel)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
            _tokenModel = tokenModel.Value ?? throw new ArgumentNullException(nameof(tokenModel));
        }

        public async Task<JwtRequestResult> AuthenticateRequest(JwtTokenRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));

            var requestResult = new JwtRequestResult()
            {
                Succeeded = false,
                UserData =  null
            };

            var user = await _userManager.FindByEmailAsync(request.Email);

            if (user == null)
            {
                return requestResult;
            }

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, false);
            if (!result.Succeeded)
            {
                return requestResult;
            }
            
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenModel.Secret));
            var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, request.Email)
            };
            
            var jwt = new JwtSecurityToken(
                issuer: _tokenModel.Issuer,
                audience: _tokenModel.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_tokenModel.AccessExpiration),
                signingCredentials: signingCredentials);


            requestResult.UserData = new UserDataDto()
            {
                Id = user.Id,
                Name = user.UserName
            };
            requestResult.Succeeded = true;
            requestResult.Token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return requestResult;
        }
    }
}
