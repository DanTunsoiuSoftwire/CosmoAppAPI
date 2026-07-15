using CosmoAppAPI.Services.Abstractions;
using CosmoAppAPI.Services.Implementations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddSingleton<IAPODServices, APODServices>();

builder.Services.AddHttpClient<IAPODServices, APODServices>(client => client.BaseAddress = new Uri("https://api.nasa.gov"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
