using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Model
{
    public class RabbitMQMessageModel
    {
        public string AssemblyFullName { get; set; }
        public string Body { get; set; }
        public int AggregateId { get; set; }
        public byte ChangedType { get; set; }
    }
    public class RabbitMQSendRequest
    {
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string Message { get; set; }
    }
    public class RabbitMQRecieveRequest
    {
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public EventHandler<BasicDeliverEventArgs> EventHandler { get; set; }
    }
}
