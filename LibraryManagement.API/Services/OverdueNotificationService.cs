using LibraryManagement.Business;
using LibraryManagement.Business.Interfaces;

namespace LibraryManagement.API.Services
{
    public class OverdueNotificationService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<OverdueNotificationService> _logger;

        public OverdueNotificationService(IServiceProvider serviceProvider, ILogger<OverdueNotificationService> logger)
        {
            _serviceProvider = serviceProvider;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Overdue Notification Service started.");

            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                try
                {
                    // Get overdue transactions
                    var overdueBooks = await unitOfWork.Transactions.GetOverdueBooksAsync();

                    foreach (var book in overdueBooks)
                    {
                        _logger.LogInformation($"Overdue Notification: Book '{book.Title}' is overdue and needs to be returned.");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while checking overdue books.");
                }

                // Wait for a day before checking again
                await Task.Delay(TimeSpan.FromHours(Constants.OVERDUE_NOTIFICATION_SERVICE_RUNNING_EVERY_HOURS), stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Overdue Notification Service is stopping.");
            return base.StopAsync(stoppingToken);
        }
    }
}
