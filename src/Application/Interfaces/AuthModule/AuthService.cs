using Domain.Entities;
using Domain.Entities.Auth;
using Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.AuthModule;

public interface IAuthService
{
	Task<User> GetUserByEmailAsync(string email);
	string GenerateToken(User user);

	public Task<RefreshToken> CreateRefreshToken(User user, string ipAddress);
	public Task<RefreshToken?> GetRefreshTokenByToken(string refreshToken);
	public Task<RefreshToken> AddRefreshToken(RefreshToken refreshToken);
	public Task DeleteOldRefreshTokens(Guid userId);
	public Task RevokeDescendantRefreshTokens(RefreshToken refreshToken, string ipAddress, string reason);

	public Task RevokeRefreshToken(RefreshToken token, string ipAddress, string? reason = null, string? replacedByToken = null);

	public Task<RefreshToken> RotateRefreshToken(User user, RefreshToken refreshToken, string ipAddress);
}
