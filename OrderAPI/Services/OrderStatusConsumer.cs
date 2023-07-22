
using MassTransit;
using SharedLibrary;

namespace OrderAPI.Consumer;

internal class OrderStatusConsumer : IConsumer<OrderStatus>
{
    public async Task Consume(ConsumeContext<OrderStatus> context)
    {
        await Console.Out.WriteLineAsync(context.Message.status);
    }
}
