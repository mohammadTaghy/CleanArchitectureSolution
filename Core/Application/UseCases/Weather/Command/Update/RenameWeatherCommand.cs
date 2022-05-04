using Application.Common;
using Application.Decorators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.UseCases.Weather
{
    public class RenameWeatherCommand : ICommand
    {
        public int Id { get; }
        public RenameWeatherCommand(int id)
        {
            Id = id;
        }

        
        [AuditLog]
        public class RenameWeatherCommandHandler : ICommandHandler<RenameWeatherCommand>
        {
            public Result Handle(RenameWeatherCommand command, CancellationToken cancellationToken)
            {                
                return Result.Ok($"The weather with id {command.Id} is updated");
            }
        }
    }
}
