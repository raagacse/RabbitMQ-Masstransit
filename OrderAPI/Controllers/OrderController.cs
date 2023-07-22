using MassTransit;
using Microsoft.AspNetCore.Mvc;
using SharedLibrary;

namespace OrderAPI.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController : ControllerBase
{
    private readonly IPublishEndpoint _publishEndPoint;
    private readonly ILogger<WeatherForecastController> _logger;

    public OrderController(IPublishEndpoint publishEndPoint, ILogger<WeatherForecastController> logger)
    {
        _publishEndPoint = publishEndPoint;
        _logger = logger;
    }

    [HttpPost]
    [Route("neworder")]
    public async Task<IActionResult> GetOrder(Order order)
    {
        await _publishEndPoint.Publish<Order>(order);
        return Ok();
    }

    [HttpPost]
    [Route("ordertype")]
    public async Task<IActionResult> OrderType(OrderType orderType)
    {
        await _publishEndPoint.Publish<OrderType>(orderType);
        return Ok();
    }
}
