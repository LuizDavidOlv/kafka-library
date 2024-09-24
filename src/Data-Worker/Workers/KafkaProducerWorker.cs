namespace data_worker
{
    public class KafkaProducerWorker : BackgroundService
    {
        private readonly ILogger<KafkaProducerWorker> _logger;

        public KafkaProducerWorker(ILogger<KafkaProducerWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
