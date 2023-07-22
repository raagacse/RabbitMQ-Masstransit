using InventoryAPI.Consumer;
using InventoryAPI.Models;
using MassTransit;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Configuration.GetValue<string>("Environment");

var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{environment}.json")
    .Build();

var rabbitMQConn = config.GetSection("RabbitMQConnection").Get<RabbitMQConnection>();

// Add services to the container.

builder.Services.AddControllers();

// builder.Services.AddMassTransit(x =>
// {
//     x.AddConsumer<OrderConsumer>();
//     x.AddConsumer<OrderTypeConsumer>();

//     x.UsingRabbitMq((ctx, cfg) =>
//     {
//         cfg.Host("amqp://user:mypass@localhost:5672");

//         cfg.ReceiveEndpoint("Order-Queue", c =>
//         {
//             c.ConfigureConsumer<OrderConsumer>(ctx);
//         });

//         cfg.ReceiveEndpoint("order-type", c =>
//         {
//             c.ConfigureConsumer<OrderTypeConsumer>(ctx);
//         });
//     });
// });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderConsumer>();
    x.AddConsumer<OrderTypeConsumer>();

    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(rabbitMQConn.hostName, h =>
        {
            h.Username(rabbitMQConn.userName);
            h.Password(rabbitMQConn.password);
        });

        config.ReceiveEndpoint("Order-Queue", c =>
        {
            c.ConfigureConsumer<OrderConsumer>(provider);
        });
        config.ReceiveEndpoint("Order-type", c =>
        {
            c.ConfigureConsumer<OrderTypeConsumer>(provider);
        });
    }));
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
