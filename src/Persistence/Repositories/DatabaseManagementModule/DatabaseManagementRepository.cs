using Application.Interfaces.DatabaseManagementModule;
using Application.Paging;
using Application.Responses;
using Domain.Entities.DatabaseManagement;
using DotNetEnv;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories.DatabaseManagementModule;

public class DatabaseManagementRepository : IDatabaseManagementService
{

	public Task<DatabaseManagementDefinations> AddAsync(DatabaseManagementDefinations entity)
	{
		throw new NotImplementedException();
	}

	public Task<ICollection<DatabaseManagementDefinations>> AddRangeAsync(ICollection<DatabaseManagementDefinations> entities)
	{
		throw new NotImplementedException();
	}

	public Task<bool> AnyAsync(Expression<Func<DatabaseManagementDefinations, bool>>? predicate = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<DatabaseManagementDefinations> DeleteAsync(DatabaseManagementDefinations entity, bool permanent = false)
	{
		throw new NotImplementedException();
	}

	public Task<ICollection<DatabaseManagementDefinations>> DeleteRangeAsync(ICollection<DatabaseManagementDefinations> entities, bool permanent = false)
	{
		throw new NotImplementedException();
	}

	public Task<DatabaseManagementDefinations?> GetAsync(Expression<Func<DatabaseManagementDefinations, bool>> predicate, Func<IQueryable<DatabaseManagementDefinations>, IIncludableQueryable<DatabaseManagementDefinations, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public Task<Paginate<DatabaseManagementDefinations>> GetListAsync(Expression<Func<DatabaseManagementDefinations, bool>>? predicate = null, Func<IQueryable<DatabaseManagementDefinations>, IOrderedQueryable<DatabaseManagementDefinations>>? orderBy = null, Func<IQueryable<DatabaseManagementDefinations>, IIncludableQueryable<DatabaseManagementDefinations, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
	{
		throw new NotImplementedException();
	}

	public IQueryable<DatabaseManagementDefinations> Query()
	{
		throw new NotImplementedException();
	}

	#region Özel Reposlar

	public async Task<GetListResponse<DatabaseManagementDefinations>> GetTablesAsync()
	{
		// JSON dosyasının yolunu belirle
		var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DatabaseManagementJson", "DatabaseManagementSettings.json");

		// Dosya yoksa hata fırlat
		if (!File.Exists(jsonFilePath))
		{
			throw new FileNotFoundException("JSON dosyası bulunamadı.", jsonFilePath);
		}

		// JSON dosyasını oku
		var json = await File.ReadAllTextAsync(jsonFilePath);

		// JSON'u deserialize et
		var tableConfig = JsonConvert.DeserializeObject<DatabaseManagementSettings>(json);

		// Schema bilgisini dinamik olarak al (örneğin, environment'dan)
		var schema = Environment.GetEnvironmentVariable("SQL_SCHEMA") ?? "dbo";

		// DatabaseManagementDefinations listesine dönüştür
		var tableDefinitions = tableConfig.Tables.Select(t => new DatabaseManagementDefinations(
			t.TableTitle,
			t.TableName,
			schema,
			t.CreateQuery.Replace("{schema}", schema),
			t.IsActive
		)).ToList();

		// GetListResponse olarak döndür
		return new GetListResponse<DatabaseManagementDefinations>
		{
			Items = tableDefinitions,
			Count = tableDefinitions.Count
		};
	}

	public async Task<string?> SetupTablels()
	{

		Env.Load();
		// Tabloları al

		var jsonFilePath = Path.Combine(Directory.GetCurrentDirectory(), "DatabaseManagementJson", "DatabaseManagementSettings.json");

		// Dosya yoksa hata fırlat
		if (!File.Exists(jsonFilePath))
		{
			throw new FileNotFoundException("JSON dosyası bulunamadı.", jsonFilePath);
		}

		// JSON dosyasını oku
		var json = await File.ReadAllTextAsync(jsonFilePath);

		// JSON'u deserialize et
		var tableConfig = JsonConvert.DeserializeObject<DatabaseManagementSettings>(json);

		// Schema bilgisini dinamik olarak al (örneğin, environment'dan)
		var schema = Environment.GetEnvironmentVariable("SQL_SCHEMA") ?? "dbo";

		// DatabaseManagementDefinations listesine dönüştür
		var tables = tableConfig.Tables.Select(t => new DatabaseManagementDefinations(
			t.TableTitle,
			t.TableName,
			schema,
			t.CreateQuery.Replace("{schema}", schema),
			t.IsActive
		)).ToList();

		// Bağlantı dizesini al
		var connectionString = Env.GetString("SQL_CONNECTION_STRING_ADRESS");

		// Paralel olarak tabloları oluştur
		await Parallel.ForEachAsync(tables, async (table, cancellationToken) =>
		{
			await CreateTableAsync(table, connectionString);
		});

		return $"{tables.Count} tablonun kurulumu başarıyla tamamlandı.";
	}

	public async Task<DatabaseManagementDefinations?> SetupModulerTables(string TableTitle)
	{
		throw new NotImplementedException();
	}


	public async Task<DatabaseManagementDefinations?> SetupTablels(string TableName)
	{
		throw new NotImplementedException();
	}


	public async Task<DatabaseManagementDefinations?> TableControlsAsync(DatabaseManagementDefinations databaseManagementDefinations)
	{

		
		// Veritabanı bağlantısı ve sorgu işlemleri için gerekli kodlar
		Env.Load();
		var connectionString = Env.GetString("SQL_CONNECTION_STRING_ADRESS");

		using (var connection = new SqlConnection(connectionString))
		{
			await connection.OpenAsync();

			// Tablonun var olup olmadığını kontrol etmek için sorgu
			var query = @"
            SELECT COUNT(*) 
            FROM INFORMATION_SCHEMA.TABLES 
            WHERE TABLE_NAME = @TableName";

			using (var command = new SqlCommand(query, connection))
			{
				command.Parameters.AddWithValue("@TableName", databaseManagementDefinations.TableName);

				// Sorguyu çalıştır ve sonucu al
				var tableExists = (int)await command.ExecuteScalarAsync() > 0;

				if (tableExists)
				{
					// Tablo varsa IsActive değerini true yap
					databaseManagementDefinations.IsActive = true;
				}
				else
				{
					// Tablo yoksa IsActive değerini false yap (veya varsayılan değeri koru)
					databaseManagementDefinations.IsActive = false;
				}
			}
		}

		return databaseManagementDefinations;
	}
	#endregion




	public Task<DatabaseManagementDefinations> UpdateAsync(DatabaseManagementDefinations entity)
	{
		throw new NotImplementedException();
	}

	public Task<ICollection<DatabaseManagementDefinations>> UpdateRangeAsync(ICollection<DatabaseManagementDefinations> entities)
	{
		throw new NotImplementedException();
	}




	public async Task CreateTableAsync(DatabaseManagementDefinations tableDefinition,string connectionFullURL)
	{

		using (var connection = new SqlConnection(connectionFullURL))
		{
			await connection.OpenAsync();

			// Tablo var mı kontrol et
			var tableExistsQuery = $@"
                    IF NOT EXISTS (SELECT * FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_NAME = '{tableDefinition.TableName}' AND TABLE_SCHEMA = '{tableDefinition.TableSchema}')
                    BEGIN
                        {tableDefinition.CreateQuery}
                    END";

			using (var command = new SqlCommand(tableExistsQuery, connection))
			{
				await command.ExecuteNonQueryAsync();
			}
		}
	}


}

public class DatabaseManagementSettings
{
	public List<DatabaseManagementDefinationsSettings> Tables { get; set; }
}
public class DatabaseManagementDefinationsSettings
{


	public string TableTitle { get; set; }
	public string TableName { get; set; }
	public string CreateQuery { get; set; }
	public bool IsActive { get; set; }
}