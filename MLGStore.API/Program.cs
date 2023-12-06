using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MLGStore.API.Extensions;
using MLGStore.Data;
using MLGStore.Entities;
using MLGStore.Services.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.ConfigureApiExtensions();
builder.Services.ConfigureCors();
builder.Services.AddControllers();
builder.Services.ConfigureSwagger();
builder.Services.ConfigureServicesExtensions(builder.Configuration);

builder.Services.AddHttpContextAccessor();
builder.Services.AddValidationErrors();
builder.Services.AddJWT(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    var passwordHasher = services.GetRequiredService<IPasswordHasher<Customer>>();
    try
    {
        var dbContext = services.GetRequiredService<StoreDbContext>();
        await dbContext.Database.MigrateAsync();
        await SeedData.InsertCustomersAsync(dbContext, loggerFactory, passwordHasher);
        await SeedData.InsertStoresAsync(dbContext, loggerFactory);
        await SeedData.InsertArticlesAsync(dbContext, loggerFactory);
    }
    catch (Exception ex)
    {
        var loggerException = loggerFactory.CreateLogger<Program>();
        loggerException.LogError(ex, "Error on migration");
    }
}

app.UseCors("CorsPolicy");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
