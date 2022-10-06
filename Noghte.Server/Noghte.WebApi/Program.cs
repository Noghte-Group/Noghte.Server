using Microsoft.EntityFrameworkCore;
using Noghte.BuildingBlock.Exceptions.Middlewares;
using Noghte.Infrastructure.ApplicationDbContext;
using Swashbuckle.AspNetCore.SwaggerUI;

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
    app.UseSwaggerUI(cfg => cfg.DocExpansion(DocExpansion.None));
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();