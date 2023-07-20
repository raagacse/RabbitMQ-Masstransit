using MassTransit;
using SharedLibrary;

namespace InventoryAPI.Consumer;
internal class OrderConsumer : IConsumer<Order>
{
    private readonly IPublishEndpoint _publisherEndPoint;
    private readonly ILogger<OrderConsumer> logger;

    public OrderConsumer(IPublishEndpoint publisherEndPoint, ILogger<OrderConsumer> logger)
    {
        _publisherEndPoint = publisherEndPoint;
        this.logger = logger;
    }
    public async Task Consume(ConsumeContext<Order> context)
    {
        //await Console.Out.WriteLineAsync(context.Message.productName);
        logger.LogInformation($"Received message: {context.Message.productName}");

        OrderStatus orderStat = new OrderStatus();
        orderStat.productId = 1;
        orderStat.status = "success";

        await _publisherEndPoint.Publish<OrderStatus>(orderStat);
    }
}
