using System.Threading.Tasks;
using MassTransit;
using Passion.Rover.Query.Consumer.Events.V1;
using Passion.Rover.Query.Consumer.Services.Contracts;


namespace Passion.Rover.Process.Consumer.Consumers
{
    public class SampleCollectedConsumer : IConsumer<SampleCollected>
    {
        private readonly IBusService _busService;
        private readonly IElasticsearchService _elasticsearchService;

        public SampleCollectedConsumer(IBusService busService, IElasticsearchService elasticsearchService)
        {
            _busService = busService;
            _elasticsearchService = elasticsearchService;
        }


        public async Task Consume(ConsumeContext<SampleCollected> context)
        {
            var message = context.Message;
            await _elasticsearchService.Write(message);
        }
    }
}