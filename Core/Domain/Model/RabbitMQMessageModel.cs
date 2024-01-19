using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model
{
    public class RabbitMQMessageModel
    {
        public string AssemblyFullName { get; set; }
        public string Body { get; set; }
        public int AggregateId { get; set; }
        public byte ChangedType { get; set; }
    }
}
