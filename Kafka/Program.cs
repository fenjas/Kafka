﻿using System;
using System.Linq;

namespace Kafka
{
    class Program
    {
        static int noOfMessages;

        static void Main(string[] args)
        {
            Console.Clear();

            if (args.Any())
            {
                try
                {
                    noOfMessages = Int32.Parse(args[1]);
                }
                catch
                {
                    noOfMessages = 1;
                }

                switch (args[0])
                {
                    case "produce":
                        {
                            new Producer().Produce(noOfMessages);
                        }
                        break;


                    case "consume":
                        {
                            new Consumer().Consume();
                        }
                        break;

                    default:
                        Console.WriteLine("Usage: kafka produce <no of messages>");
                        Console.WriteLine("Usage: kafka consume");
                        break;
                }
            }
        }

    }
}
