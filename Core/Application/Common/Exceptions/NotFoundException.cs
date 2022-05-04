using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public sealed class NotFoundException : Exception
    {
        public NotFoundException(string name, object key)
            : base(String.Format(CommonMessage.NotFound,name))
        {
        }
    }
}
