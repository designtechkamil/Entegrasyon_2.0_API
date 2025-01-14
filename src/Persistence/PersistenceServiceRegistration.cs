using Application.Interfaces.ConnectionModule;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using Persistence.Repositories.ConnectionModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Persistence.Repositories.EntegrasyonModulu.WTPartRepositories;
using Application.Interfaces.EntegrasyonModulu.WTPartServices;
using Application.Interfaces.UsersModule;
using Persistence.Repositories.UsersModule;
using Application.Interfaces.AuthModule;
using Persistence.Repositories.AuthModule;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Application.Interfaces.ConnectionModule.WindchillConnectionModule;
using Persistence.Repositories.ConnectionModule.WindchillConnectionModule;
using Application.Interfaces.DatabaseManagementModule;
using Persistence.Repositories.DatabaseManagementModule;
using Application.Interfaces.WindchillModule;
using Persistence.Repositories.WindchillModule;
using Application.Interfaces.LogModule;
using Persistence.Repositories.LogModule;

namespace Persistence;

public static class PersistenceServiceRegistration
{
	public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<BaseDbContexts>((serviceProvider, options) =>
		{
			options.UseSqlServer(serviceProvider.GetRequiredService<IConfiguration>().GetConnectionString(configuration["SQL_CONNECTION_STRING_ADRESS"]))
				   .ReplaceService<IModelCacheKeyFactory, DynamicSchemaModelCacheKeyFactory>();
		}, ServiceLifetime.Scoped);

		services.AddAuthentication(options =>
		{
			options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
			options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
		})
		.AddJwtBearer(options =>
		{
			options.TokenValidationParameters = new TokenValidationParameters
			{
				ValidateIssuer = true,
				ValidateAudience = true,
				ValidateLifetime = true,
				ValidateIssuerSigningKey = true,
				ValidIssuer = configuration["Jwt:Issuer"],
				ValidAudience = configuration["Jwt:Audience"],
				IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(configuration["Jwt:SecretKey"]))
			};
		});
		services.AddHttpClient();
		services.AddScoped<ILogService, LogRepository>();
		services.AddScoped<IWindchillService, WindchillRepository>();
		services.AddScoped<IDatabaseManagementService, DatabaseManagementRepository>();
		services.AddScoped<IAuthService, AuthRepository>();
		services.AddScoped<IUserService, UserRepository>();
		services.AddScoped<IWTPartService, WTPartRepository>();
		services.AddScoped<IStateService, StateRepository>();
		services.AddScoped<IConnectionService, ConnectionRepository>();
		services.AddScoped<IWindchillConnectionService, WindchillConnectionRepository>();

		return services;
	}
}
