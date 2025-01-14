using Application.Interfaces.LogModule;
using Domain.Entities.LogSettings;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.LogModule;

public class LogRepository : ILogService
{
	private readonly BaseDbContexts _dbContexts;

	public LogRepository(BaseDbContexts dbContexts)
	{
		_dbContexts = dbContexts;
	}

	public async Task<List<LogEntry>> GetLogsByDateAsync(DateTime date)
	{
		return await _dbContexts.Logs
			.Where(log => log.TimeStamp.Date == date.Date) 
			.ToListAsync(); 
	}
}
