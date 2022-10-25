using MassTransit;
using Microsoft.EntityFrameworkCore;
using Noghte.Application.Configuration.Mapper;
using Noghte.BuildingBlock;
using Noghte.BuildingBlock.Extensions;
using Noghte.BuildingBlock.Middlewares;
using Noghte.Infrastructure.ApplicationDbContext;
using Noghte.WebApi.Middleswares;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.InjectLifeCycles();

builder.Services.InitializeAutoMapper();

builder.Services.AddDbContext<NoghteDbContext>(cfg =>
{
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

#region Masstransit

const string domainCommandName = nameof(IContract);
var assembly = AppDomain.CurrentDomain.GetAssemblies()
           .Single(assembly => assembly.GetName().Name == "Noghte.Application");


var requestClients = assembly.GetTypes()
    .Where(t => t.GetInterface(domainCommandName) is not null && t.Name.Contains(domainCommandName) is false)
    .Distinct()
    .ToList();

builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(assembly);
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

app.UseMiddleware<JwtMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();