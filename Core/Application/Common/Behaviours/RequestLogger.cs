using Application.Common.Interfaces;
using Common;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Behaviours
{
    public class RequestLogger<TRequest> : IRequestPreProcessor<TRequest>
    {
        private readonly ILogger _logger;
        private readonly ICurrentUserSession _currentUserSession;

        public RequestLogger(ILogger<TRequest> logger, ICurrentUserSession currentUserSession)
        {
            _logger = logger;
            _currentUserSession = currentUserSession;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var name = typeof(TRequest).Name;

            _logger.LogInformation("PreRequest Log: {Name} {@UserId} {@Request}",
                name, _currentUserSession.UserId, request);

            return Task.CompletedTask;
        }
    }
}
