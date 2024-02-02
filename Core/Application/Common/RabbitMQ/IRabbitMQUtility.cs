using Application.Common.Model;
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
        void RecieveMessage(RabbitMQRecieveRequest rabbitMQRecieveRequest);
        bool SendMessage(RabbitMQSendRequest rabbitMQSendRequest);
    }
}
