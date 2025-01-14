using AutoMapper;
using Domain.Entities;
using Domain.Entities.Auth;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AuthModule;

public class AuthManager : IAuthService
{
	public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken)
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

	public string GenerateToken(User user)
	{
		throw new NotImplementedException();
	}

	public Task<RefreshToken?> GetRefreshTokenByToken(string refreshToken)
	{
		throw new NotImplementedException();
	}

	public Task<User> GetUserByEmailAsync(string email)
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