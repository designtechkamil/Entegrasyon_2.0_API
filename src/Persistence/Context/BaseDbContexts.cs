using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using DotNetEnv;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Domain.Entities;
using Domain.Entities.Auth;
using Domain.Entities.LogSettings;

namespace Persistence.Context;

public class DynamicSchemaModelCacheKeyFactory : IModelCacheKeyFactory
{

	public object Create(DbContext context, bool designTime)
	{
		if (context is BaseDbContexts dbContext)
		{
			return (context.GetType(), dbContext.schemaName);
		}

		return context.GetType();
	}
}


public class BaseDbContexts : DbContext
{
	public string schemaName { get; private set; }
	protected IConfiguration Configuration { get; set; }

	public DbSet<WTPart> WTParts { get; set; }
	public DbSet<User> Users { get; set; }
	public DbSet<LogEntry> Logs { get; set; }


	public BaseDbContexts(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
	{
		Env.Load();
		Configuration = configuration;
		Database.EnsureCreated();
	}

	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
	{
		Env.Load();
		string connectionString = Env.GetString("SQL_CONNECTION_STRING_ADRESS");

		optionsBuilder.UseSqlServer(connectionString);
		schemaName = Env.GetString("SQL_SCHEMA");
	}

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		modelBuilder.HasDefaultSchema(schemaName);
		modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}