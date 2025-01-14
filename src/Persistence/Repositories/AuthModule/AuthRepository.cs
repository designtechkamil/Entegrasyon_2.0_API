using Application.Interfaces.AuthModule;
using Application.Interfaces.UsersModule;
using Domain.Entities;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Security.JWT;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.AuthModule;

public class AuthRepository : IAuthService
{

	private readonly IUserService _userService;
	private readonly IConfiguration _configuration;

	public AuthRepository(IUserService userService, IConfiguration configuration)
	{
		_userService = userService;
		_configuration = configuration;
	}


	public async Task<User> GetUserByEmailAsync(string email)
	{
		return await _userService.GetByEmail(email);
	}

	public string GenerateToken(User user)
	{
		var tokenHandler = new JwtSecurityTokenHandler();
		var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

		var tokenDescriptor = new SecurityTokenDescriptor
		{
			Subject = new ClaimsIdentity(new[]
			{
		new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
		new Claim(ClaimTypes.Email, user.Email),
		new Claim(ClaimTypes.Name, user.FullName),
		new Claim(ClaimTypes.Role, user.Role.ToString())
	}),
			Expires = DateTime.UtcNow.AddDays(7),
			Issuer = _configuration["Jwt:Issuer"],
			Audience = _configuration["Jwt:Audience"],
			SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
		};

		var token = tokenHandler.CreateToken(tokenDescriptor);
		return tokenHandler.WriteToken(token);
	}

	//public string GenerateToken(User user)
	//{
	//	var tokenHandler = new JwtSecurityTokenHandler();
	//	var key = Encoding.ASCII.GetBytes(_configuration["Jwt:SecretKey"]);

	//	var tokenDescriptor = new SecurityTokenDescriptor
	//	{
	//		Subject = new ClaimsIdentity(new[]
	//		{
	//		new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
	//		new Claim(ClaimTypes.Email, user.Email),
	//		new Claim(ClaimTypes.Name, user.FullName)
	//	}),
	//		Expires = DateTime.UtcNow.AddDays(7), // Token süresi (örnek: 7 gün)
	//		Issuer = _configuration["Jwt:Issuer"],
	//		Audience = _configuration["Jwt:Audience"],
	//		SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
	//	};

	//	var token = tokenHandler.CreateToken(tokenDescriptor);
	//	return tokenHandler.WriteToken(token);
	//}

	public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
	{
		throw new NotImplementedException();
	}

	public Task<AccessToken> CreateAccessToken(User user)
	{
		throw new NotImplementedException();
	}

	public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress)
	{
		throw new NotImplementedException();
	}

	public Task DeleteOldRefreshTokens(Guid userId)
	{
		throw new NotImplementedException();
	}



	public Task<RefreshToken?> GetRefreshTokenByToken(string refreshToken)
	{
		throw new NotImplementedException();
	}



	public Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason)
	{
		throw new NotImplementedException();
	}

	public Task RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null)
	{
		throw new NotImplementedException();
	}

	public Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress)
	{
		throw new NotImplementedException();
	}
}
