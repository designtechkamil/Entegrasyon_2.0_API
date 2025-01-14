using Application.Pipelines.Logging;
using Application.Pipelines.RequireSystemCheck;
using Application.Pipelines.SqlConnectionCheck;
using Application.Pipelines.Transaction;
using Application.Pipelines.Validation;
using CrossCuttingConcerns.Serilog;
using CrossCuttingConcerns.Serilog.Logger;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Application;

public static class ApplicationServiceRegistration
{
	public static IServiceCollection AddApplicationServices(this IServiceCollection services)
	{

		services.AddAutoMapper(Assembly.GetExecutingAssembly());
		services.AddSubClassesOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));


		services.AddMediatR(cfg =>
		{
			cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
			cfg.AddOpenBehavior(typeof(SystemCheckBehavior<,>));
			cfg.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
			cfg.AddOpenBehavior(typeof(LoggingBehavior<,>));
			cfg.AddOpenBehavior(typeof(TransactionScopeBehavior<,>));
			cfg.AddOpenBehavior(typeof(SqlConnectionCheckBehavior<,>));
		});


		//services.AddSingleton<LoggerServiceBase, FileLogger>();
		services.AddSingleton<LoggerServiceBase, MsSqlLogger>();









		return services;
	}

	public static IServiceCollection AddSubClassesOfType(
		this IServiceCollection services,
		Assembly assembly,
		Type type,
		Func<IServiceCollection, Type, IServiceCollection>? addWithLifeCycle = null
		)
	{
		var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
		foreach (var item in types)
			if (addWithLifeCycle == null)
				services.AddScoped(item);
			else
				addWithLifeCycle(services, type);
		return services;
	}
}
