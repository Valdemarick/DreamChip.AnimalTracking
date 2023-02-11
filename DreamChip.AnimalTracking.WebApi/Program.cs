using DreamChip.AnimalTracking.DAL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDAL(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.

await DatabaseCreator.CreateDatabase(builder.Services);

app.UseHttpsRedirection();

app.Run();
