using Application.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
namespace Application.Interfaces.EntegrasyonModulu.WTPartServices;

public interface IWTPartService
{
	Task<ICollection<WTPart>> GetState();
	Task<WTPart> GetPart(string stateType);
	Task<WTPart> GetPartID(long ParcaPartID);
	Task<WTPart> SendWTPartAsync(string stateType);
	Task SendWTPartAlternate();
	Task SendWTPartEquivalence();
	Task SendWTPartToERP(string stateType);

	//Özel Kullanılacak olan atlttaki yukarıdakilerin hepsi rastgele deneme olanlar altakinin mantığını çözüp onu kullanıcaz

	Task<WTPart?> GetAsync(
	DbContext? context,
	Expression<Func<WTPart, bool>> predicate,
	Func<IQueryable<WTPart>, IIncludableQueryable<WTPart, object>>? include = null,
	bool withDeleted = false,
	bool enableTracking = true,
	CancellationToken cancellationToken = default);

	Task<Paginate<WTPart>> GetListAsync(
		Expression<Func<WTPart, bool>>? predicate = null,
		Func<IQueryable<WTPart>, IOrderedQueryable<WTPart>>? orderBy = null,
		Func<IQueryable<WTPart>, IIncludableQueryable<WTPart, object>>? include = null,
		int index = 0,
		int size = 10,
		bool withDeleted = false,
		bool enableTracking = true,
		CancellationToken cancellationToken = default);

	Task<WTPart> DeleteAsync(DbContext? context,WTPart entity, bool permanent = false);
}
