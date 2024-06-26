using Middleware;
using VideoCatalogueAPI.Model;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddJwtAuthentication(builder.Configuration);
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
//mywebapp port
app.UseCors(policy => policy.WithOrigins("http://localhost:5070")
	.AllowAnyMethod()
	.AllowAnyHeader()
);
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
