using Kafka.Models;

namespace Kafka.Interfaces
{
    public interface IKafkaProducerService<TKey, TValue>
    {
        Task PublishAsync(TKey key, TValue value, CancellationToken token);
    }
}
