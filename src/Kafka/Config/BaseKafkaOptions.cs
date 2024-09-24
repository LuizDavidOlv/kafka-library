using Kafka.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Kafka.Config
{
    public class BaseKafkaOptions : IKafkaOptions
    {
        [Required]
        public required string BootstrapServers { get; set; }

        [Required]
        public required string Topic { get; set; }

        public KafkaAuthentication? Authentication { get; set; }
    }
}
