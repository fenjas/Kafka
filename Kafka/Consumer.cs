using System;
using System.Threading;
using Confluent.Kafka;

namespace Kafka
{
    class Consumer : KakfaSettings
    {
        static CancellationTokenSource cts = new CancellationTokenSource();
        static ConsumerConfig consumerConfig = null;

        void CreateConfig()
        {
            consumerConfig = new ConsumerConfig
            {
                BootstrapServers = kafkaServer,
                GroupId = groupID,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };
        }

        void CreateConsumerAndConsume()
        {

            var cb = new ConsumerBuilder<string, string>(consumerConfig);

            Console.WriteLine("Press Ctrl+C to exit");

            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);

            using (var consumer = cb.Build())
            {
                consumer.Subscribe(kafkaTopicName);

                try
                {
                    while (!cts.IsCancellationRequested)
                    {
                        var cr = consumer.Consume(cts.Token);
                        var offset = cr.TopicPartitionOffset;
                        Console.WriteLine($"Message '{cr.Value}' at: '{offset}'.");
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    consumer.Close();
                }
            }
        }

        static void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            args.Cancel = true;
            Console.WriteLine("In OnExit");
            cts.Cancel();

        }

        public void Consume()
        {
            CreateConfig();
            CreateConsumerAndConsume();
        }
    }
}
