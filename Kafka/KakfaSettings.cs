using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kafka
{
    class KakfaSettings
    {
        public const string kafkaServer = "localhost:9092";
        public const string groupID = "cars-group";
        public const string kafkaTopicName = "cars";
        public const string messagesFile = "cars.txt";
    }
}
