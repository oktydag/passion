using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Passion.Rover.Command.Commands;
using Passion.Rover.Command.Domain.Services.Contracts;

namespace Passion.Rover.Command.Handlers
{
    public class GoGivenLocationCommandHandler : IRequestHandler<GoGivenLocationCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly IMovementDomainService _movementDomainService;
        private readonly ILogger<GoGivenLocationCommandHandler> _logger;

        public GoGivenLocationCommandHandler(IMediator mediator, IMovementDomainService movementDomainService,
            ILogger<GoGivenLocationCommandHandler> logger)
        {
            _mediator = mediator;
            _movementDomainService = movementDomainService;
            _logger = logger;
        }

        public async Task<bool> Handle(GoGivenLocationCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _movementDomainService.Go(request.X, request.Y, request.Direction);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                return false;
            }

            return true;
        }
    }
}