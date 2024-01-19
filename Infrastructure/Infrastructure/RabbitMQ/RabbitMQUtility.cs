using Application.Common;
using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RabbitMQ
{
    public class RabbitMQUtility : IRabbitMQUtility
    {
        private static IModel _channel;
        private Object _object;
        private IModel channel
        {
            get
            {
                if (_channel == null)
                {
                    lock (_object)
                    {
                        if (_channel == null)
                        {
                            ConnectionFactory factory = new ConnectionFactory() { HostName = "localhost" };
                            IConnection connection = factory.CreateConnection();
                            _channel = connection.CreateModel();
                        }
                    }
                }
                return _channel;
            }
        }
        public void RecieveMessage(string queueName, EventHandler<BasicDeliverEventArgs> eventHandler)
        {
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);

            Console.WriteLine(" [*] Waiting for messages.");

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += eventHandler;
            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
        }
        public bool SendMessage(string queueName, string message)
        {
            channel.QueueDeclare(queue: queueName, durable: false, exclusive: false, autoDelete: false, arguments: null);
            var body = Encoding.UTF8.GetBytes(message);
            channel.BasicPublish(exchange: "", routingKey: queueName, basicProperties: null, body: body);
            Console.WriteLine($" [x] Sent {message}");
            return true;
        }
    }
}
