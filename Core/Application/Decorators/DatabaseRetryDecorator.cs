using System;
using Application.Common;


namespace Application.Decorators
{
    public sealed class DatabaseRetryDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;        
        // To Do: Make config for this property
        private readonly int NumberOfDatabaseRetries = 3;
        public DatabaseRetryDecorator(ICommandHandler<TCommand> handler)
        {        
            _handler = handler;
        }

        public Result Handle(TCommand command,CancellationToken cancellationToken)
        {
            for (int i = 0; ; i++)
            {
                try
                {
                    Result result = _handler.Handle(command, cancellationToken);
                    return result;
                }
                catch (Exception ex)
                {
                    if (i >= NumberOfDatabaseRetries || !IsDatabaseException(ex))
                        throw;
                }
            }
        }

        private bool IsDatabaseException(Exception exception)
        {
            string message = exception.InnerException?.Message;

            if (message == null)
                return false;

            return message.Contains("The connection is broken and recovery is not possible")
                || message.Contains("error occurred while establishing a connection");
        }
    }
}
