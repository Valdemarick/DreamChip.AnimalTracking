using DreamChip.AnimalTracking.Application;
using DreamChip.AnimalTracking.DAL;
using DreamChip.AnimalTracking.WebApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDAL(builder.Configuration)
    .AddApplication()
    .AddWebApi();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

await DatabaseCreator.CreateDatabase(builder.Services);

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
