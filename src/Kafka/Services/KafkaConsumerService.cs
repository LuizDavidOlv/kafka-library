using Confluent.Kafka;
using Kafka.Config;
using Kafka.Interfaces;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Kafka.Services
{
    public class KafkaConsumerService<TKey, TValue> : BackgroundService
    {
        private readonly KafkaConsumerOptions _options;

        private readonly IConsumer<TKey, TValue> _consumer;

        protected readonly ILogger _logger;
        private readonly IKafkaConfigBuilder _configBuilder;

        protected KafkaConsumerService(IOptions<KafkaConsumerOptions> options, ILogger<KafkaConsumerService<TKey, TValue>> logger, IKafkaConfigBuilder configBuilder = null)
        {
            _logger = logger;
            _options = options.Value;
            _configBuilder = configBuilder ?? new KafkaConfigBuilder();
            _consumer = BuildConsumer();
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Yield();
            _consumer.Subscribe(_options.Topic);
            await StartConsumption(stoppingToken);
        }

        protected virtual async Task StartConsumption(CancellationToken stoppingToken)
        {
            await Consume(stoppingToken);
        }

        protected async Task Consume(CancellationToken tokken)
        {
            try
            {
                while (!tokken.IsCancellationRequested)
                {
                    var result = _consumer.Consume();

                    if (result == null)
                    {
                        _logger.LogWarning("Consume result is null");
                        continue;
                    }

                    if (result.IsPartitionEOF)
                    {
                        _logger.LogWarning("Reached end of partition");
                        continue;
                    }

                    bool num = await HandleMessageAsync(result.Message.Key, result.Message.Value, tokken);
                    _logger.LogInformation($"Consumed message {result.Message.Key} : {result.Message.Value}");
                    if (num)
                    {
                        _consumer.StoreOffset(result);
                    }
                }
            }
            catch (ConsumeException ex)
            {
                _logger.LogInformation(ex, "Error consuming message for consumer {consumerName} {consumerMemberId} {@consumerRecord} {@error} {topics}", _consumer.Name, _consumer.MemberId, ex.ConsumerRecord, ex.Error, _consumer.Subscription);
                throw;
            }
            catch (Exception exception2)
            {
                _logger.LogCritical(exception2, "An uncaught exception occurred during message handling.");
            }
        }

        protected virtual async Task<bool> HandleMessageAsync(TKey key, TValue value, CancellationToken stoppingToken)
        {
            await Task.Yield();
            return HandleMessage(key, value);
        }

        protected virtual bool HandleMessage(TKey key, TValue value)
        {
            throw new NotImplementedException();
        }

        private IConsumer<TKey, TValue> BuildConsumer()
        {
            var config = _configBuilder.BuildConsumerConfig(_options);
            var consumer = new ConsumerBuilder<TKey, TValue>(config).Build();

            return consumer;
        }
    }
}
