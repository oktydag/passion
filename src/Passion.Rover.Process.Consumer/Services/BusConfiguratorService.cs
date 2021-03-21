using System;
using MassTransit;
using Passion.Rover.Process.Consumer.Consumers;
using Passion.Rover.Process.Consumer.Services.Contracts;

public class BusConfiguratorService : IBusConfiguratorService
{
    private readonly IRoverService _roverService;
    
    public BusConfiguratorService(IRoverService roverService)
    {
        _roverService = roverService;
    }
    
    IBusControl _bus;

    public bool Start()
    {
        _bus = ConfigureBus(_roverService);
        _bus.Start(TimeSpan.FromSeconds(1000));

        return true;
    }
    

    public bool Stop()
    {
        _bus?.Stop(TimeSpan.FromSeconds(30));

        return true;
    }

   private IBusControl ConfigureBus(IRoverService roverService)
    {
        return Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri(ConnectionConstants.HostAddress), k =>
            {
                k.Username(ConnectionConstants.Username);
                k.Password(ConnectionConstants.Password);
            });
            cfg.ReceiveEndpoint(ConnectionConstants.SubscribeQueueName, e =>
            {
                e.Consumer( () => new PhotoWasTakenConsumer(roverService));
                e.Consumer( () => new LocationChangedConsumer(roverService));
                e.Consumer( () => new SampleCollectedConsumer(roverService));
            });
        });
    }
}



public class ConnectionConstants
{
    public static string HostAddress = "rabbitmq://localhost/";
    public static string Username = "od";
    public static string Password = "od1234";
    public static string SubscribeQueueName = "Out.Passion.Command";
    public static string PublishSagaQueueName = "In.Passion.Command.Status";
}
