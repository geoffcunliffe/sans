using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Sans.CreditUnion.API.Features.Authentication.Models;
using Sans.CreditUnion.API.Infrastructure.Constants;
using Sans.CreditUnion.API.Services.Interfaces;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Sans.CreditUnion.API.Features.Authentication
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthenticationController : Controller
    {
        private readonly ISignInService _signInService;
        private readonly IConfiguration _configuration;

        public AuthenticationController(ISignInService signInService, IConfiguration configuration)
        {
            _signInService = signInService;
            _configuration = configuration;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<AuthenticateResult>> Post([FromBody] AuthenticateRequest model)
        {
            var signInResult = await _signInService.CheckPasswordAsync(model.Email, model.Password);

            if (!signInResult.Result.Succeeded)
                return BadRequest(signInResult);

            var claims = new[]
            {
              new Claim(JwtRegisteredClaimNames.Sub, signInResult.User.Id) ,
              new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
              new Claim(JwtClaimTypes.MemberId, signInResult.User.MemberId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(_configuration["Jwt:Issuer"],
              _configuration["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(20),
              signingCredentials: credentials);

            return new AuthenticateResult { Jwt = new JwtSecurityTokenHandler().WriteToken(token) };
        }
    }
}