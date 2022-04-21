using Logico.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Logger Service
builder.Services.ConfigureLoggerService();

// Add DB Context
builder.Services.ConfigureMySqlContext(builder.Configuration);

// Add Repository Wrapper
builder.Services.ConfigureRepositoryWrapper();

// Add Controllers
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// For Logger Service
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
