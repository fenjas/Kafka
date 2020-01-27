using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using Confluent.Kafka;

namespace Kafka
{
    class Producer : KakfaSettings
    {
        static readonly AutoResetEvent _closing = new AutoResetEvent(false);
        static IProducer<string, string> producer = null;
        static ProducerConfig producerConfig = null;
        static List<string> randomMessages = new List<string>() { };

        void CreateConfig()
        {
            producerConfig = new ProducerConfig { BootstrapServers = kafkaServer };
        }

        void CreateProducer()
        {
            var pb = new ProducerBuilder<string, string>(producerConfig);
            producer = pb.Build();
        }

        async void SendMessage(string topic, string message)
        {
            var msg = new Message<string, string>
            {
                Key = null,
                Value = message
            };

            var delRep = await producer.ProduceAsync(topic, msg);
            var topicOffset = delRep.TopicPartitionOffset;

            Console.WriteLine($"Delivered '{delRep.Value}' to: {topicOffset}");
        }

        void OnExit(object sender, ConsoleCancelEventArgs args)
        {
            Console.WriteLine("Exit");
            _closing.Set();
        }


        public void Produce(int noOfMessages)
        {
            CreateConfig();
            CreateProducer();
            GenerateRandomMessages();

            for (int i = 0; i < noOfMessages; i++)
            {
                SendMessage(kafkaTopicName, randomMessages[new Random((int)DateTime.Now.Millisecond).Next(0, randomMessages.Count - 1)]);
                Thread.Sleep(800);
            }

            Console.WriteLine("Press Ctrl+C to exit");

            Console.CancelKeyPress += new ConsoleCancelEventHandler(OnExit);
            _closing.WaitOne();

        }

        void GenerateRandomMessages()
        {

            try
            {
                using (StreamReader sr = new StreamReader(Path.GetDirectoryName(System.AppDomain.CurrentDomain.BaseDirectory) + "\\" + messagesFile))
                {
                    string line;

                    while (!string.IsNullOrEmpty(line = sr.ReadLine()))
                    {
                        randomMessages.Add(line);
                    }
                }
            }
            catch
            {
                throw;
            }
        }
    }
}