using Microsoft.Extensions.Logging;
using NLog;
using NLog.Fluent;
using Serilog;
using System.Text.Json.Serialization;
using System.Text.Json;
using TodoApplication;
using TodoApplication.Filters;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using Log = Serilog.Log;
using Microsoft.OpenApi.Models;

var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.SetIsOriginAllowed(origin => new Uri(origin).Host == "localhost")
                .AllowAnyHeader().AllowAnyMethod();
                      });
});

// Add services to the container.
builder.Services.ConfigureDependencyInjection(builder.Configuration);

//Config AutoMapper
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllersWithViews();

// Configure lowercase routing
builder.Services.AddRouting(opt => opt.LowercaseUrls = true);

//Filter
builder.Services.AddScoped<ValidationFilter>();

builder.Services.AddControllers().AddJsonOptions(o =>
{
    o.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    o.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // configure Bearer Authentication in Swagger
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Description = "Bearer Authentication with JWT Token",
        Type = SecuritySchemeType.Http,
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        },
                        new List<string>()
                    }
                });
});

var app = builder.Build();

//Config logger
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Debug()
    .WriteTo.File("logs/TodoApplication.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

//ref: https://stackoverflow.com/questions/71562958/how-to-create-a-logger-in-net-6-program-cs
var serviceCollection = new ServiceCollection();
serviceCollection.AddLogging();
var serviceProvider = serviceCollection.BuildServiceProvider();
#pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
ILogger _logger = serviceProvider.GetService<ILogger<Program>>();
#pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(MyAllowSpecificOrigins);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
