using ECommerce.Api.Cart.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDistributedRedisCache(options =>
{
    options.Configuration = "redis";
    options.InstanceName = "cart_api";
});

builder.Services.AddScoped<ICartService, CartService>();

var app = builder.Build();


    app.UseSwagger();
    app.UseSwaggerUI();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();