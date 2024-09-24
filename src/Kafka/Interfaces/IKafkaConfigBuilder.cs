using Confluent.Kafka;
using Kafka.Config;

namespace Kafka.Interfaces
{
    public interface IKafkaConfigBuilder
    {
        ConsumerConfig BuildConsumerConfig(KafkaConsumerOptions options);
        ProducerConfig BuildProducerConfig(KafkaProducerOptions options);
    }
}
