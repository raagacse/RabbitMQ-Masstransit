using MassTransit;
using SharedLibrary;

namespace InventoryAPI.Consumer;
internal class OrderConsumer : IConsumer<Order>
{
    private readonly ILogger<OrderConsumer> logger;

    public OrderConsumer(ILogger<OrderConsumer> logger)
    {
        this.logger = logger;
    }
    public async Task Consume(ConsumeContext<Order> context)
    {
        await Console.Out.WriteLineAsync(context.Message.productName);
        logger.LogInformation($"Received message: {context.Message.productName}");
    }
}
