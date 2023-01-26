using ECommerce.Api.Dashboard.Data;
using ECommerce.Api.Dashboard.Dtos;
using ECommerce.Api.Dashboard.Entities;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace ECommerce.Api.Dashboard.Services;

public class MessageService : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private AppDbContext _context;
    private IConnection _connection;
    private IModel _channel;
    private string _queueName;

    public MessageService(IServiceScopeFactory scopeFactory )
    {
        _scopeFactory = scopeFactory; 
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _context = _scopeFactory.CreateScope().ServiceProvider.GetRequiredService<AppDbContext>();

        var factory = new ConnectionFactory
        {
            HostName = "localhost",
            UserName = "guest",
            Password = "guest",
            Port = 5672,
            DispatchConsumersAsync = true
        };

        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

  
        _channel.ExchangeDeclare("product_added", ExchangeType.Fanout);
        _queueName = _channel.QueueDeclare().QueueName;

        _channel.QueueBind(_queueName, "product_added", "");

        HandleQueue();

    }
    private void HandleQueue()
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (sender, args) =>
        {
          //  args.RoutingKey
            var productJson = Encoding.UTF8.GetString(args.Body.ToArray());
            var product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductQueue>(productJson);
            await SaveProductAsync(product);
        };

        _channel.BasicConsume(_queueName, false, consumer);
    }

    private async Task SaveProductAsync(ProductQueue productModel)
    {
        var product = new Product()
        {
            ProductId = productModel.Id,
            ProductName = productModel.Name,
            ProductDescription = productModel.Description,
            ProductPrice = productModel.Price,
            ProductCount = productModel.Count,
            ProductImageUrl = productModel.ImageUrl,
            ProductBrand = productModel.Brand.Name,
            ProductCategory = productModel.Category.Name
        };

        _context.Products?.Add(product);
        await _context.SaveChangesAsync();

       // await _hubContext.Clients.All.SendAsync("ProductAdded", product);
    }

}
