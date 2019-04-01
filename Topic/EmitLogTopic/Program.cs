using RabbitMQ.Client;
using System;
using System.Linq;
using System.Text;

namespace EmitLogTopic
{
    class Program
    {
        public static void Main(string[] args)
    {
        var factory = new ConnectionFactory() { HostName = "localhost" };
        using(var connection = factory.CreateConnection())
        using(var channel = connection.CreateModel())
        {
            channel.ExchangeDeclare(exchange: "topic_logs",
                                    type: "topic");

                while (true)
                {
                    Console.WriteLine("Enter your routing key :");
                    var routingKey = Console.ReadLine();
                    Console.WriteLine("Enter your message :");
                    var message = Console.ReadLine();
                    var body = Encoding.UTF8.GetBytes(message);
                    channel.BasicPublish(exchange: "topic_logs",
                                         routingKey: routingKey,
                                         basicProperties: null,
                                         body: body);
                    Console.WriteLine(" [x] Sent '{0}':'{1}'", routingKey, message);
                }
        }
    }
    }
}
