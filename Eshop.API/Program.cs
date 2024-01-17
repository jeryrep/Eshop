using Eshop.Application;
using Eshop.Infrastructure;
using Microsoft.OpenApi.Models;
using MongoDB.Bson;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegistryInfrastructure(builder.Configuration);
builder.Services.RegistryApplication();

builder.Services
    .AddAutoMapper(typeof(Program))
    .AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly))
    .AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
});

var app = builder.Build();

if (builder.Environment.IsDevelopment()) app.UseDeveloperExceptionPage();

app.UseRouting();
app.MapControllers();

app
    .UseSwagger()
    .UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
    c.RoutePrefix = string.Empty;
});

BsonDefaults.GuidRepresentationMode = GuidRepresentationMode.V3;

await app.RunAsync();