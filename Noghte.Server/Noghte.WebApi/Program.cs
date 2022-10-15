using System.Reflection;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Noghte.BuildingBlock;
using Noghte.BuildingBlock.Exceptions;
using Noghte.BuildingBlock.Exceptions.Middlewares;
using Noghte.BuildingBlock.Extensions;
using Noghte.Infrastructure.ApplicationDbContext;
using Swashbuckle.AspNetCore.SwaggerUI;
using Microsoft.Extensions.DependencyInjection;
using Noghte.Application.Extensions;
using StackExchange.Redis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddDbContext<NoghteDbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

#region Masstransit

const string domainCommandName = nameof(IContract);
var assemblies = typeof(ReflectionExtensions).Assembly;


var requestClients = assemblies.GetTypes()
    .Where(t => t.GetInterface(domainCommandName) is not null && t.Name.Contains(domainCommandName) is false)
    .Distinct()
    .ToList();

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(assemblies);
    requestClients.ForEach(message => { cfg.AddRequestClient(message); });
    // cfg.ConfigureMediator((context, cfg) => { cfg.UseConsumeFilter(typeof(ValidationFilter<>), context); });
});

#endregion

#region Redis

var redisConnectionString = builder.Configuration.GetConnectionString("RedisConnection");
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = redisConnectionString;
    options.InstanceName = "";
});

builder.Services.AddSingleton<IConnectionMultiplexer>(provider => ConnectionMultiplexer.Connect(redisConnectionString));

#endregion

var app = builder.Build();

app.UseCustomExceptionHandler();

var scopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
var scope = scopeFactory.CreateScope();

var noghteDbContext = scope.ServiceProvider.GetRequiredService<NoghteDbContext>();

#region Initializing

noghteDbContext.Database.Migrate();

#endregion

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(cfg => { cfg.DocExpansion(DocExpansion.None); });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();