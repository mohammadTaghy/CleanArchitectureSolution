using Application.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.IUtils
{
    public interface IMessages
    {
        Result Dispatch(ICommand command);
        Result Dispatch(ICommand command, CancellationToken cancellationToken);
        T Dispatch<T>(IQuery<T> query);
    }
}
