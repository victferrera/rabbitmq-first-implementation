using System.Text;
using RabbitMQ.Client;

namespace Send
{
    internal sealed class Send
    {
        internal void SendMessage()
        {
            var factory = new ConnectionFactory { 
                HostName = "localhost",
                UserName = "YOUR_USERNAME",
                Password = "YOUR_PASSWORD"
            };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();

            channel.QueueDeclare(
                queue: "Hello",
                durable: false,
                exclusive: false,
                autoDelete: false,
                arguments: null);

            const string message = "Hello World, from RabbitMQ :D";
            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(
                exchange: string.Empty,
                routingKey: "Hello",
                basicProperties: null,
                body: body);

            Console.WriteLine($"[X] Send {message}");

            Console.WriteLine("Press enter to exit.");
            Console.ReadLine();
        }
    }
}
