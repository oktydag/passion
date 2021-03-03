using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Passion.Rover.Command.Commands;
using Passion.Rover.Command.Domain.Services.Contracts;

namespace Passion.Rover.Command.Handlers
{
    public class TakeWhatYouSeeCommandHandler : IRequestHandler<TakeWhatYouSeeCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ICameraDomainService _cameraDomainService;
        private readonly ILogger<TakeWhatYouSeeCommandHandler> _logger;

        public TakeWhatYouSeeCommandHandler(IMediator mediator, ICameraDomainService cameraDomainService,
            ILogger<TakeWhatYouSeeCommandHandler> logger)
        {
            _mediator = mediator;
            _cameraDomainService = cameraDomainService;
            _logger = logger;
        }

        public async Task<bool> Handle(TakeWhatYouSeeCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _cameraDomainService.TakeWhatYouSee(request.ObjectName);
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