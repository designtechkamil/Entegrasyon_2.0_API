using Application.Interfaces.EntegrasyonModulu.WTPartServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Infrastructure.BackgroundServices;

public class IntegrationBackgroundService : BackgroundService
{
	private readonly ILogger<IntegrationBackgroundService> _logger;
	private readonly IServiceProvider _serviceProvider;

	public IntegrationBackgroundService(ILogger<IntegrationBackgroundService> logger, IServiceProvider serviceProvider)
	{
		_logger = logger;
		_serviceProvider = serviceProvider;
	}

	protected override async Task ExecuteAsync(CancellationToken stoppingToken)
	{
		while (!stoppingToken.IsCancellationRequested)
		{
			using (var scope = _serviceProvider.CreateScope())
			{
				var stateService = scope.ServiceProvider.GetRequiredService<IStateService>();

				// Her durum için ayrı bir görev başlat
				var releasedTask = stateService.RELEASED(stoppingToken);
				//var inworkTask = stateService.INWORK(stoppingToken);
				//var cancelledTask = stateService.CANCELLED(stoppingToken);

				// Tüm görevlerin tamamlanmasını bekle
				await Task.WhenAll(releasedTask);
				//await Task.WhenAll(releasedTask, inworkTask, cancelledTask);
			}

			// Örnek bir bekleme süresi
			await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
		}
	}
}