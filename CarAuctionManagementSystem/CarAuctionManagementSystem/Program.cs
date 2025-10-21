using CarAuctionManagementSystem.Handlers.Command;
using CarAuctionManagementSystem.Handlers.Command.Interfaces;
using CarAuctionManagementSystem.Handlers.Query;
using CarAuctionManagementSystem.Handlers.Query.Interfaces;
using CarAuctionManagementSystem.Persistence;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using System;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
builder.Services.AddDbContext<ApplicationDbContext>();
builder.Services.AddTransient<IVehicleQuery, QueryVehicleHandler>();
builder.Services.AddTransient<IVehicleCommand, CommandVehicleHandler>();
builder.Services.AddTransient<IAuctionCommand, CommandAuctionHandler>();
builder.Services.AddTransient<IBidCommand, CommandBidHandler>();


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    SeedData.EnsureSeedData(db);
}

app.UseSwagger();
app.UseSwaggerUI();


app.UseAuthorization();

app.MapControllers();


app.Run();
