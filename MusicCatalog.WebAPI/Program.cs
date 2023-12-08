using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.AspNetCore.Mvc.Formatters;
using MusicCatalog.WebAPI.Extensions;
using MusicCatalog.WebAPI.Filters.Authentication;
using MusicCatalog.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddLoggingMiddleware();

// builder.Services.AddApiKeyAuthentication();
builder.Services.AddApiKeyAuthFilter();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseLoggingMiddleware();
// app.UseApiKeyAuthentication();

app.UseAuthorization();

app.MapControllers();

app.MapGroup("api/songs").AddSongsEndpoints();

app.Run();