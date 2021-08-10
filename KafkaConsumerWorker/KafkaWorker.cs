using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

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
	    _logger.LogInformation("Registry by Kenan BAŞDEMİR");
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
