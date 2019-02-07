using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Common.Entities;
using DAL.Repositories.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MobileStore.Authorization;

namespace MobileStore.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AuthController : ControllerBase
	{
		private const string LoginTakenErrorMessage = "Login is already taken";
		private const string LoginNotCorrectErrorMessage = "No users with this login";
		private const string PasswordErrorMessgae = "Wrong password";

		IUserRepository _userRepository;
		public AuthController(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

		[AllowAnonymous]
		[HttpPost("[action]")]
		public IActionResult Register([FromBody]User userToRegistry)
		{
			IActionResult response = BadRequest();
			User user = _userRepository.GetUserByLogin(userToRegistry.Login);
			if (user != null)
			{
				return BadRequest(new { error = LoginTakenErrorMessage });
			}
			_userRepository.Create(userToRegistry);

			var tokenString = GenerateJSONWebToken(_userRepository.GetUserByLogin(userToRegistry.Login));
			response = Ok(new { token = tokenString });


			return response;
		}

		[AllowAnonymous]
		[HttpPost("[action]")]
		public IActionResult Login([FromBody]User login)
		{
			IActionResult response = Unauthorized();
			var user = _userRepository.GetUserByLogin(login.Login);
			if (user == null)
			{
				return Unauthorized(new { error = LoginNotCorrectErrorMessage });
			}
			if (user.Password != login.Password)
			{
				return Unauthorized(new { error = PasswordErrorMessgae });
			}

			var tokenString = GenerateJSONWebToken(user);

			return Ok(new { token = tokenString });
		}

		public string GenerateJSONWebToken(User userInfo)
		{
			var securityKey = AuthOptions.GetSymmetricSecurityKey();
			var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

			var claims = new[] {
						new Claim(JwtRegisteredClaimNames.Sub, userInfo.Login),
						new Claim(JwtRegisteredClaimNames.Jti, userInfo.Id.ToString())
						};

			var token = new JwtSecurityToken(AuthOptions.ISSUER,
			AuthOptions.AUDIENCE,
			claims,
			expires: DateTime.Now.AddMinutes(120),
			signingCredentials: credentials);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}