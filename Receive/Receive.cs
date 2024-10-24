using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Receive
{
    internal class Receive
    {
        internal void ReadMessages()
        {
            var factory = new ConnectionFactory {
                HostName = "localhost",
                UserName = "YOUR_USERNAME",
                Password = "YOUR_PASSWORD"
            };

            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "Hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null
                );

            Console.WriteLine("[] Waiting for message");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                Console.WriteLine($"[X] Received {message}");
            };

            channel.BasicConsume(queue: "Hello", autoAck: true, consumer: consumer);

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
