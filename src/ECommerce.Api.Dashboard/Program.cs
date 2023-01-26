using ECommerce.Api.Dashboard.Data;
using ECommerce.Api.Dashboard.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddHostedService<MessageService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseNpgsql("Host=localhost;Port=5432;Username=postgres;Password=3321;database=dashboard");
});


var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
