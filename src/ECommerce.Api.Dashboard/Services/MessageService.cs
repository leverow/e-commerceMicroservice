using ECommerce.Api.Dashboard.Data;
using ECommerce.Api.Dashboard.Dtos;
using ECommerce.Api.Dashboard.Entities;
using Mapster;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
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
    //private string _queueName;

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
        var _queueName = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(_queueName, "product_added","");
        HandleQueue(_queueName);

        _channel.ExchangeDeclare("product_updated", ExchangeType.Fanout);
        var queueName2 = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queueName2, "product_updated", "");
        HandleQueue(queueName2);

        _channel.ExchangeDeclare("product_deleted", ExchangeType.Fanout);
        var queueName3 = _channel.QueueDeclare().QueueName;
        _channel.QueueBind(queueName3, "product_deleted", "");

        HandleQueue(queueName3);

    }
    private void HandleQueue(string queueName)
    {
        var consumer = new AsyncEventingBasicConsumer(_channel);
        consumer.Received += async (sender, args) =>
        {
            var exchangeName = args.RoutingKey;
            var productJson = Encoding.UTF8.GetString(args.Body.ToArray());
            var product = Newtonsoft.Json.JsonConvert.DeserializeObject<ProductQueue>(productJson);

            if(exchangeName == "product_added")
            {
                await SaveProductAsync(product);
            }
            else if (exchangeName == "product_updated")
            {
                await UpdateProductAsync(product);
            }
            else if (exchangeName == "product_deleted")
            {
                await DeleteProductAsync(product);
            }
        };

        _channel.BasicConsume(queueName, false, consumer);
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
    }

    private async Task UpdateProductAsync(ProductQueue productModel)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productModel.Id);

        product.ProductId = productModel.Id;
        product.ProductName = productModel.Name;
        product.ProductDescription = productModel.Description;
        product.ProductPrice = productModel.Price;
        product.ProductCount = productModel.Count;
        product.ProductImageUrl = productModel.ImageUrl;
        product.ProductBrand = productModel.Brand.Name;
        product.ProductCategory = productModel.Category.Name;
      
        _context.Products?.Update(product);
        await _context.SaveChangesAsync();
    }

    private async Task DeleteProductAsync(ProductQueue productModel)
    {
        var product = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == productModel.Id);

        _context.Products?.Remove(product);
        await _context.SaveChangesAsync();
    }

}
