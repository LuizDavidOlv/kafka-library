using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Models
{
    public abstract class KafkaMessage
    {
        [Required]
        public Guid CorrelationID { get; set; }

        [Required]
        public required string SourceApplicationName { get; set; }

        [Required]
        public required string SourceApplicationID { get; set; }

        [Required]
        public DateTime TransmissionDateTime { get; set; } = DateTime.UtcNow;

        [Required]
        public required string BusinessProcessName { get; set; }

        [Required]
        public DateTime DataExpiry { get; set; }

        public string? VaultKeyPath { get; set; }

        public int? VaultVersion { get; set; }

        public string? VaultKeySaltValue { get; set; }
    }
}
