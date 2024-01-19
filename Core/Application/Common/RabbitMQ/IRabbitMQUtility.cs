using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common
{
    public interface IRabbitMQUtility
    {
        void RecieveMessage(string queueName, EventHandler<BasicDeliverEventArgs> eventHandler);
        bool SendMessage(string queueName, string message);
    }
}
