using System;
using Application.Common;

namespace Application.Decorators
{
    public sealed class AuditLoggingDecorator<TCommand> : ICommandHandler<TCommand>
        where TCommand : ICommand
    {
        private readonly ICommandHandler<TCommand> _handler;

        public AuditLoggingDecorator(ICommandHandler<TCommand> handler)
        {
            _handler = handler;
        }

        public Result Handle(TCommand command, CancellationToken cancellationToken)
        {
            // To Do: Use proper logging here
            Console.WriteLine($"Command of type {command.GetType().Name}: has been logged");

            return _handler.Handle(command, cancellationToken);
        }
    }
}
