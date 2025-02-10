using Application;
using Core.CrossCuttingConcerns.Exception.WebApi.Extensions;
using Core.Localization.WebApi;
using Persistence;
using Core.Persistence.WebApi;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddHttpContextAccessor();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(opt =>
    opt.AddDefaultPolicy(p =>
    {
        p.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    })
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwaggerUI(options => options.SwaggerEndpoint("/openapi/v1.json", "OpenAPI v1"));
}

if (app.Environment.IsProduction())
    app.ConfigureCustomExceptionMiddleware();

app.UseDbMigrationApplier();

app.MapGet("/", () => "Hello World!");
app.MapControllers();

app.UseHttpsRedirection();

app.UseResponseLocalization();

app.Run();