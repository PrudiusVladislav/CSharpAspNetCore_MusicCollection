using System.Text.Json.Serialization;
using MusicCatalog.WebAPI.Extensions;
using MusicCatalog.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

builder.Services.AddDependencies(builder.Configuration);
builder.Services.AddLoggingMiddleware();
builder.Services.AddApiKeyAuthentication();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
app.UseApiKeyAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();