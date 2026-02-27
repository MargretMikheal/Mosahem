using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using mosahem.Persistence;
using Mosahem.Application.Interfaces;
using Mosahem.Application.Settings;

namespace mosahem.Presistence.BackgroundServices
{
    public class TemporaryFilesCleanupService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<TemporaryFilesCleanupService> _logger;
        private readonly IOptionsMonitor<TempFilesCleanupSettings> _settings;

        public TemporaryFilesCleanupService(
            IServiceScopeFactory scopeFactory,
            ILogger<TemporaryFilesCleanupService> logger,
            IOptionsMonitor<TempFilesCleanupSettings> settings)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
            _settings = settings;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var settings = _settings.CurrentValue;

                if (settings.Enabled)
                {
                    await CleanupTempFilesAsync(stoppingToken);
                }

                await Task.Delay(TimeSpan.FromHours(Math.Max(1, settings.RunEveryHours)), stoppingToken);
            }
        }

        private async Task CleanupTempFilesAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<MosahmDbContext>();
            var fileService = scope.ServiceProvider.GetRequiredService<IFileService>();

            var tempFiles = await dbContext.TemporaryFileUploads
                .AsTracking()
                .ToListAsync(cancellationToken);

            if (tempFiles.Count == 0)
                return;

            foreach (var tempFile in tempFiles)
            {
                try
                {
                    await fileService.DeleteFileAsync(tempFile.FileKey, cancellationToken);
                    dbContext.TemporaryFileUploads.Remove(tempFile);
                }
                catch (Exception ex)
                {
                    _logger.LogWarning(ex, "Failed to cleanup temporary file {FileKey}.", tempFile.FileKey);
                }
            }

            await dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
