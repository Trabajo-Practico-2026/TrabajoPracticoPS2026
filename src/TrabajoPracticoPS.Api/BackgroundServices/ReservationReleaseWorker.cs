using TrabajoPracticoPS.Application.Interfaces;
using TrabajoPracticoPS.Infrastructure.Persistence.Repositories;

namespace TrabajoPracticoPS.Api.BackgroundServices
{
    public class ReservationReleaseWorker : BackgroundService
    {
        private readonly ILogger<ReservationReleaseWorker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public ReservationReleaseWorker(ILogger<ReservationReleaseWorker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Reservation Release Worker started at: {time}", DateTimeOffset.Now);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    using (var scope = _serviceProvider.CreateScope())
                    {
                        // Nota: Asegúrate de que el nombre de la interfaz esté bien escrito (Repository)
                        var reservationRepository = scope.ServiceProvider.GetRequiredService<IReservationRespository>();

                        _logger.LogInformation("Checking for expired reservations...");

                        // Ejecutamos la lógica basada en la columna ExpiresAt
                        int affectedRows = await reservationRepository.ReleaseExpiredReservationsAsync(stoppingToken);

                        if (affectedRows > 0)
                        {
                            _logger.LogInformation("Successfully expired {count} reservations.", affectedRows);
                        }
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while releasing expired reservations.");
                }

                // Esperar 1 minuto para la siguiente revisión
                await Task.Delay(TimeSpan.FromMinutes(1), stoppingToken);
            }
        }
    }
}
