using Application.Interfaces.EntegrasyonModulu.WTPartServices;
using Azure;
using Domain.Entities;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Context;
using System;
using System.Diagnostics.Metrics;
using System.Text.Json;
using System.Threading.Tasks;

namespace Persistence.Repositories.EntegrasyonModulu.WTPartRepositories;

public class StateRepository : IStateService
{
    private readonly BaseDbContexts _context;
    private readonly IWTPartService _wTPartService;
    private const string FilePath = "deneme.json";
	private readonly IServiceProvider _serviceProvider;

	public StateRepository(BaseDbContexts context, IWTPartService wTPartService, IServiceProvider serviceProvider)
	{
		_context = context;
		_wTPartService = wTPartService;
		_serviceProvider = serviceProvider;
	}

	public async Task RELEASED(CancellationToken cancellationToken)
	{
		try
		{
			var scope1 = _serviceProvider.CreateScope();
			var scope2 = _serviceProvider.CreateScope();

			var context1 = scope1.ServiceProvider.GetRequiredService<BaseDbContexts>();
			var context2 = scope2.ServiceProvider.GetRequiredService<BaseDbContexts>();
			WTPart response = await _wTPartService.GetAsync(context1, predicate: x => x.ParcaState == "RELEASED");

			if (response is not null)
			{
				var message = $"SIRA {response.ParcaPartID} - {response.ParcaName} - {response.ParcaNumber} - {response.ParcaState} - {response.EntegrasyonDurum} - {response.ParcaVersion}";
				await AppendToJsonFile(message);
				await _wTPartService.DeleteAsync(context2, response, permanent: false);
			}
			else
			{
				var message = $"RELEASED a ait veri yok";
				await AppendToJsonFile(message);
			}

			// İşlemler burada


		}
		catch (Exception ex)
		{
			// Hata loglama
			await AppendToJsonFile($"RELEASED işleminde hata oluştu: {ex.Message}");
		}
		finally
		{
			await Task.Delay(1000, cancellationToken); // 1 saniye gecikme
		}
	}

    public async Task INWORK(CancellationToken cancellationToken)
    {

		try
		{
			var scope1 = _serviceProvider.CreateScope();
			var scope2 = _serviceProvider.CreateScope();

			var context1 = scope1.ServiceProvider.GetRequiredService<BaseDbContexts>();
			var context2 = scope2.ServiceProvider.GetRequiredService<BaseDbContexts>();
			WTPart response = await _wTPartService.GetAsync(context1, predicate: x => x.ParcaState == "INWORK");

			if (response is not null)
			{
				var message = $"SIRA {response.ParcaPartID} - {response.ParcaName} - {response.ParcaNumber} - {response.ParcaState} - {response.EntegrasyonDurum} - {response.ParcaVersion}";
				await AppendToJsonFile(message);
				await _wTPartService.DeleteAsync(context2, response, permanent: false);
			}
			else
			{
				var message = $"INWORK a ait veri yok";
				await AppendToJsonFile(message);
			}

			// İşlemler burada


		}
		catch (Exception ex)
		{
			// Hata loglama
			await AppendToJsonFile($"RELEASED işleminde hata oluştu: {ex.Message}");
		}
		finally
		{
			await Task.Delay(1000, cancellationToken); // 1 saniye gecikme
		}

	}

    public async Task CANCELLED(CancellationToken cancellationToken)
    {
		try
		{
			var scope1 = _serviceProvider.CreateScope();
			var scope2 = _serviceProvider.CreateScope();
			
				var context1 = scope1.ServiceProvider.GetRequiredService<BaseDbContexts>();
				var context2 = scope2.ServiceProvider.GetRequiredService<BaseDbContexts>();
				WTPart response = await _wTPartService.GetAsync(context1, predicate: x => x.ParcaState == "CANCELLED");

				if (response is not null)
				{
					var message = $"SIRA {response.ParcaPartID} - {response.ParcaName} - {response.ParcaNumber} - {response.ParcaState} - {response.EntegrasyonDurum} - {response.ParcaVersion}";
					await AppendToJsonFile(message);
					await _wTPartService.DeleteAsync(context2, response, permanent: false);
				}
				else
				{
					var message = $"CANCELLED a ait veri yok";
					await AppendToJsonFile(message);
				}
			
			// İşlemler burada


		}
		catch (Exception ex)
		{
			// Hata loglama
			await AppendToJsonFile($"RELEASED işleminde hata oluştu: {ex.Message}");
		}	
		finally
		{
			await Task.Delay(1000, cancellationToken); // 1 saniye gecikme
		}
	}


    private async Task AppendToJsonFile(string message)
    {
        var logData = new
        {
            Message = message,
            Timestamp = DateTime.UtcNow
        };
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };
        string jsonString = JsonSerializer.Serialize(logData, options);

        using (StreamWriter sw = File.AppendText(FilePath))
        {
            await sw.WriteLineAsync(jsonString);
        }
    }
}