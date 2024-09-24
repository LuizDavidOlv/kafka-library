using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Kafka.Application.Serializers;
using Kafka.Config;
using Kafka.Interfaces;
using Kafka.Models;
using Microsoft.Extensions.Logging;
using System.Text;

namespace Kafka.Services
{
    public class KafkaProducerService<TKey, TValue> : IKafkaProducerService<TKey, TValue> where TValue : notnull, KafkaMessage
    {
        private readonly IProducer<TKey, TValue> _producer;
        private readonly KafkaProducerOptions _options;
        private readonly IKafkaConfigBuilder _configBuilder;


        public KafkaProducerService(KafkaProducerOptions options, IKafkaConfigBuilder configBuilder)
        {
            _options = options;
            _configBuilder = configBuilder ?? new KafkaConfigBuilder();
            _producer = BuildProducer();
        }

        private IProducer<TKey, TValue> BuildProducer()
        {
            var config = _configBuilder.BuildProducerConfig(_options);
            var producerBuilder = new ProducerBuilder<TKey, TValue>(config);

            if (typeof(TKey) != typeof(Null) && typeof(TKey) != typeof(string))
            {
                producerBuilder = producerBuilder.SetKeySerializer(new Serializer<TKey>());
            }
            if (typeof(TValue) != typeof(Null) && typeof(TValue) != typeof(string))
            {
                producerBuilder = producerBuilder.SetValueSerializer(new Serializer<TValue>());
            }

            return producerBuilder.Build();
        }


        public async Task<ProduceResult> PublishAsync(TKey key, TValue value, CancellationToken token)
        {

            Message<TKey, TValue> message = new()
            {
                Key = key,
                Value = value
            };

            var deliveryReport = await _producer.ProduceAsync(_options.Topic, message, token);

            if (_options.FlushTimespan > TimeSpan.Zero)
                _producer.Flush(_options.FlushTimespan);


            return ProduceResult.Pass;

        }
    }
}
