using MassTransit;
using SharedLibrary;

namespace InventoryAPI.Consumer;

internal class OrderTypeConsumer : IConsumer<OrderType>
{
    private readonly ILogger<OrderTypeConsumer> logger;

    public OrderTypeConsumer(ILogger<OrderTypeConsumer> logger)
    {
        this.logger = logger;
    }

    public async Task Consume(ConsumeContext<OrderType> context)
    {
        await Console.Out.WriteLineAsync(context.Message.productTypeName);
        logger.LogInformation($"OrderTypeConsumer: {context.Message.productTypeName}");
    }
}