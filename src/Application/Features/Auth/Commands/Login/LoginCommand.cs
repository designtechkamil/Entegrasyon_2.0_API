using Application.Features.Auth.Rules;
using Application.Interfaces.AuthModule;
using Application.Interfaces.UsersModule;
using Application.Pipelines.Logging;
using AutoMapper;
using Domain.Entities;
using Domain.Entities.Auth;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Security.Enums;
using Security.Hashing;
using Security.JWT;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Auth.Commands.Login;

public class LoginCommand : IRequest<LoggedResponse>, ILoggableRequest
{
	public string Email { get; set; }
	public string Password { get; set; }

	public string LogMessage { get; set; } = string.Empty;



	public class LoginCommandHandler : IRequestHandler<LoginCommand, LoggedResponse>
	{
		private readonly IAuthService _authService;
		private readonly IMapper _mapper;

		public LoginCommandHandler(IAuthService authService, IMapper mapper)
		{
			_authService = authService;
			_mapper = mapper;
		}

		public async Task<LoggedResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
		{
			User? user = await _authService.GetUserByEmailAsync(request.Email);

			// Kullanıcı yoksa veya şifre yanlışsa hata fırlat
			if (user == null || !HashingHelper.VerifyPasswordHash(request.Password, user.PasswordHash, user.PasswordSalt))
			{
				request.LogMessage = "Giris islemi basarisiz. Email veya sifre hatali.";
				throw new UnauthorizedAccessException("Email veya şifre hatalı.");
			}

			// Token oluştur (örnek olarak JWT token)
			var token = _authService.GenerateToken(user);

			// Set the log message before returning the response
			request.LogMessage = $"Giris islemi basarili. Kullanici: {user.Email}";

			// LoginResponse döndür
			return new LoggedResponse
			{
				Id = user.Id,
				Email = user.Email,
				FullName = user.FullName,
				Token = token
			};
		}
	}
}