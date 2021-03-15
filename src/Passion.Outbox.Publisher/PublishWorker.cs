using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Passion.Outbox.Publisher.Events;
using Passion.Outbox.Publisher.Services.Contracts;
using Passion.Outbox.Publisher.Settings;

namespace Passion.Outbox.Publisher
{
    public class PublishWorker
    {
        private readonly ILogger<PublishWorker> _logger;
        private readonly IOutboxService _outboxService;
        private readonly IMessageBusService _messageBusService;
        private readonly IEventFactory _eventFactory;
        private readonly IProcessSettings _processSettings;

        public PublishWorker(ILogger<PublishWorker> logger, IOptions<ProcessSettings> processSettings,
            IOutboxService outboxService,
            IEventFactory eventFactory, IMessageBusService messageBusService, IProcessSettings processSettings1)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _processSettings = processSettings.Value;
            _outboxService = outboxService;
            _eventFactory = eventFactory;
            _messageBusService = messageBusService;
        }

        public async Task Run(string[] args)
        {
            _logger.LogInformation("Starting...");

            try
            {
                for (int i = 0; i < _processSettings.GetExecutionLimit(); i++)
                {
                    var dataInOutbox = _outboxService.Process().Result;
                    if (dataInOutbox == null ) break;
                    
                    var eventType = _eventFactory.FindEventType(dataInOutbox.Type);
                    var publishedData = JsonConvert.DeserializeObject(dataInOutbox.Data, eventType);

                    await _messageBusService.Publish(publishedData);

                    _logger.LogInformation("Message published.");

                    await _outboxService.MarkAsPending(dataInOutbox.Id);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                _logger.LogError($"Finished with error : {ex.Message}");

                throw;
            }

            _logger.LogInformation("Finished...");

            await Task.CompletedTask;
        }
    }
}