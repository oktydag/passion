using System;
using System.Threading.Tasks;
using MassTransit;
using Passion.Outbox.Publisher.Consumers;
using Passion.Outbox.Publisher.Services;

namespace Passion.Outbox.Publisher
{
    public class ConsumerWorker
    {
        public async Task Run(string[] args)
        {
            try
            {
                var bus = BusConfigurator.Instance
                    .ConfigureBus((cfg, host) =>
                    {
                        cfg.ReceiveEndpoint(host, ConnectionConstants.ConsumeQueueName,
                            e => { e.Consumer<MessageProcessedConsumer>(); });
                    });

                bus.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            await Task.CompletedTask;
        }
    }
}