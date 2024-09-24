using System.ComponentModel.DataAnnotations;

namespace Kafka.Config
{
    public class KafkaProducerOptions : BaseKafkaOptions
    {
        [Required]
        public TimeSpan FlushTimespan { get; set; } = TimeSpan.FromSeconds(10.0);
    }
}
