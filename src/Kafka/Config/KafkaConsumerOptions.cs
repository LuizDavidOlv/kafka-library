using Confluent.Kafka;
using Kafka.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace Kafka.Config
{
    public class KafkaConsumerOptions : BaseKafkaOptions
    {
        [Required]
        public required string GroupId { get; set; }

        public AutoOffsetReset AutoOffsetReset { get; set; } = AutoOffsetReset.Earliest;


        public PartitionAssignmentStrategy PartitionAssignmentStrategy { get; set; } = PartitionAssignmentStrategy.CooperativeSticky;


        public int SessionTimeoutMs { get; set; } = Convert.ToInt32(TimeSpan.FromSeconds(6.0).TotalMilliseconds);


        public int MaxPollIntervalMs { get; set; } = Convert.ToInt32(TimeSpan.FromMinutes(10.0).TotalMilliseconds);

    }
}
