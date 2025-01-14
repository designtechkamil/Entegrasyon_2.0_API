using Application.Interfaces.EntegrasyonModulu.WTPartServices;
using Application.Paging;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.EntegrasyonModulu.WTPartRepositories;

public class WTPartRepository : IWTPartService
{

	private readonly BaseDbContexts _dbContexts;

	public WTPartRepository(BaseDbContexts dbContexts)
	{
		_dbContexts = dbContexts;
	}


	public async Task<ICollection<WTPart>> GetState()
	{
		return await _dbContexts.WTParts.ToListAsync();
	}

	public async Task<WTPart> GetPart(string stateType)
	{
		return await _dbContexts.WTParts.Where(x => x.ParcaState == stateType).FirstOrDefaultAsync();
	}


	public async Task SendWTPartAlternate()
	{
		throw new NotImplementedException();
	}

	public async Task<WTPart> SendWTPartAsync(string stateType)
	{
		WTPart wTPart = await GetPart(stateType);

		var sayac = 1;
		Console.WriteLine(sayac+" ---- "+ wTPart);
		sayac = sayac + 1;
		return wTPart;
	}

	public async Task SendWTPartEquivalence()
	{
		throw new NotImplementedException();
	}

	public async Task SendWTPartToERP(string stateType)
	{
		var sayac = 1;
		Console.WriteLine(sayac + " ---- " + stateType);
		sayac = sayac + 1;
	}

	public Task<WTPart> GetPartID(long ParcaPartID)
	{
		throw new NotImplementedException();
	}

	//Özel olanlar alttaki mantığını çözdükten sonra o tarz olamları kullanıcaz

	public async Task<WTPart?> GetAsync(DbContext? context,Expression<Func<WTPart, bool>> predicate, Func<IQueryable<WTPart>, IIncludableQueryable<WTPart, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
	{
		IQueryable<WTPart> queryable = Query();
		if (!enableTracking)
			queryable = queryable.AsNoTracking();
		if (include != null)
			queryable = include(queryable);
		if (withDeleted)
			queryable = queryable.IgnoreQueryFilters();
		return await queryable.FirstOrDefaultAsync(predicate, cancellationToken);
	}

	public async Task<WTPart> DeleteAsync(DbContext? context, WTPart entity, bool permanent = false)
	{
		 _dbContexts.WTParts.Remove(entity);
		_dbContexts.SaveChanges();
		return entity;
	}

	public IQueryable<WTPart> Query()
	{
		return _dbContexts.Set<WTPart>();
	}

	public async Task<Paginate<WTPart>> GetListAsync(Expression<Func<WTPart, bool>>? predicate = null, Func<IQueryable<WTPart>, IOrderedQueryable<WTPart>>? orderBy = null, Func<IQueryable<WTPart>, IIncludableQueryable<WTPart, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
	{
		IQueryable<WTPart> queryable = Query();
		if (!enableTracking)
			queryable = queryable.AsNoTracking();
		if (include != null)
			queryable = include(queryable);
		if (withDeleted)
			queryable = queryable.IgnoreQueryFilters();
		if (predicate != null)
			queryable = queryable.Where(predicate);
		if (orderBy != null)
			return await orderBy(queryable).ToPaginateAsync(index, size, cancellationToken);
		return await queryable.ToPaginateAsync(index, size, cancellationToken);
	}
}
