using System;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Logging;
using Passion.Rover.Command.Commands;
using Passion.Rover.Command.Domain.Services.Contracts;

namespace Passion.Rover.Command.Handlers
{
    public class CollectSampleCommandHandler : IRequestHandler<CollectSampleCommand, bool>
    {
        private readonly IMediator _mediator;
        private readonly ISampleCollectorDomainService _sampleCollectorDomainService;
        private readonly ILogger<CollectSampleCommandHandler> _logger;

        public CollectSampleCommandHandler(IMediator mediator, ISampleCollectorDomainService sampleCollectorDomainService,
            ILogger<CollectSampleCommandHandler> logger)
        {
            _mediator = mediator;
            _sampleCollectorDomainService = sampleCollectorDomainService;
            _logger = logger;
        }

        public async Task<bool> Handle(CollectSampleCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await _sampleCollectorDomainService.Collect(request.ObjectName, request.ObjectAmount);
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