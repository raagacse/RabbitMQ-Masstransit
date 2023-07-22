using MassTransit;
using OrderAPI.Consumer;
using OrderAPI.Models;

var builder = WebApplication.CreateBuilder(args);

var environment = builder.Configuration.GetValue<string>("Environment");

Console.WriteLine(environment);

var config = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.{environment}.json")
    .Build();

var rabbitMQConn = config.GetSection("RabbitMQConnection").Get<RabbitMQConnection>();


// Add services to the container.
builder.Services.AddControllers();

// builder.Services.AddMassTransit(x =>
// {
//     x.AddConsumer<OrderStatusConsumer>();

//     x.UsingRabbitMq((ctx, cfg) =>
//     {
//         cfg.Host("amqp://user:mypass@localhost:5672");

//         cfg.ReceiveEndpoint("Order-Status", c =>
//         {
//             c.ConfigureConsumer<OrderStatusConsumer>(ctx);
//         });
//     });
// });

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<OrderStatusConsumer>();

    x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(config =>
    {
        config.Host(rabbitMQConn.hostName, h =>
        {
            h.Username(rabbitMQConn.userName);
            h.Password(rabbitMQConn.password);
        });

        config.ReceiveEndpoint("Order-Status", c =>
        {
            c.ConfigureConsumer<OrderStatusConsumer>(provider);
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
