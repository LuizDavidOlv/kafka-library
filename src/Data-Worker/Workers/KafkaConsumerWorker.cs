namespace data_worker
{
    public class KafkaConsumerWorker : BackgroundService
    {
        private readonly ILogger<KafkaConsumerWorker> _logger;

        public KafkaConsumerWorker(ILogger<KafkaConsumerWorker> logger)
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
