using Confluent.Kafka;
using Kafka.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka.Config
{
    public class KafkaConfigBuilder() : IKafkaConfigBuilder
    {
        public virtual ConsumerConfig BuildConsumerConfig(KafkaConsumerOptions options)
        {
            var consumerConfig = new ConsumerConfig
            {
                //This option is used to read all messages from the beginning
                //AutoOffsetReset = AutoOffsetReset.Earliest,
                //This option is used to read only new messages
                //AutoOffsetReset = AutoOffsetReset.Latest,
                GroupId = options.GroupId,
                AutoCommitIntervalMs = 5000,
                ReconnectBackoffMs = 50,
                ReconnectBackoffMaxMs = 1000,
                BootstrapServers = options.BootstrapServers,
                AutoOffsetReset = options.AutoOffsetReset,
                EnableAutoCommit = true,
                EnableAutoOffsetStore = false,
                Acks = Acks.Leader,
                EnablePartitionEof = true,
                PartitionAssignmentStrategy = options.PartitionAssignmentStrategy,
                SessionTimeoutMs = options.SessionTimeoutMs,
                MaxPollIntervalMs = options.MaxPollIntervalMs,
                //IsolationLevel = IsolationLevel.ReadCommitted,
            };

            return consumerConfig;
        }

        public virtual ProducerConfig BuildProducerConfig(KafkaProducerOptions options)
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = options.BootstrapServers,
                ReconnectBackoffMs = 50,
                ReconnectBackoffMaxMs = 1000,
                MessageSendMaxRetries = 3,
                //EnableIdempotence = true, //This option is used to send messages in order
                //MaxInFlight = 1,
                //Acks = Acks.All,
                //
                //Acks = Acks.Leader,
            };

            return producerConfig;
        }

    }
}
