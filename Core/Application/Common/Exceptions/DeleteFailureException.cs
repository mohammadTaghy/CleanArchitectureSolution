using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public sealed class DeleteFailureException : Exception
    {
        public DeleteFailureException(string name, object key)
            : base(string.Format(CommonMessage.DeleteFailure,name,key))
        {
        }
    }
}
