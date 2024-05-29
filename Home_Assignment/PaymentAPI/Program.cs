using Google.Api;
using Middleware;
using PaymentAPI.Controllers;
using PaymentAPI.Model;
using PaymentAPI.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));
builder.Services.Configure<SubGCPSettings>(builder.Configuration.GetSection("GCP"));
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSingleton<PaymentService>();
builder.Services.AddHostedService<SubscriberService>();

builder.Services.AddControllers();
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
