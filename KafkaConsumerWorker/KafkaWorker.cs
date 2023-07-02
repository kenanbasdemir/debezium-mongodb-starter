using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace KafkaConsumerWorker
{
    public class KafkaWorker : BackgroundService
    {
        private readonly ILogger<KafkaWorker> _logger;
        private readonly string topic = "dbserver1.inventory.customers";

        public KafkaWorker(ILogger<KafkaWorker> logger)
        {
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Issues: https://github.com/kenanbasdemir/debezium-mongodb-starter/issues");
            _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);

            var conf = new ConsumerConfig
            {
                GroupId = "messageConsumer",
                BootstrapServers = "127.0.0.1:9094",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
            using (var builder = new ConsumerBuilder<Ignore,
                string>(conf).Build())
            {
                builder.Subscribe(topic);
                try
                {
                    while (true)
                    {
                        var consumer = builder.Consume(stoppingToken);
                        Console.WriteLine($"Message: {consumer.Message.Value} received from {consumer.TopicPartitionOffset}");
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError($"{ex.Message}{DateTimeOffset.Now}");
                    builder.Close();
                }
            }
        }
    }


}
