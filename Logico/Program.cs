using Logico;
using Logico.Extensions;
using NLog;
using VueCliMiddleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Add Controllers
builder.Services.AddControllers();

// Add DB Context
builder.Services.ConfigureMySqlContext(builder.Configuration);

// Logger Service
builder.Services.ConfigureLoggerService();

// Add Repository Wrapper
builder.Services.ConfigureRepositoryWrapper();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.ConfigureAuthentication(builder.Configuration);

// Add Vue files
builder.Services.AddSpaStaticFiles(options =>
{
    options.RootPath = "clientapp/dist";
});

builder.Services.AddCors(o => o.AddPolicy("MyPolicy", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

// For Logger Service
LogManager.LoadConfiguration(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.  
    app.UseHsts();
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

// Enable Cors
app.UseCors("MyPolicy");

app.UseSpaStaticFiles();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.UseSpa(spa =>
{
    if (app.Environment.IsDevelopment())
        spa.Options.SourcePath = "clientapp/";
    else
        spa.Options.SourcePath = "dist";
    if (app.Environment.IsDevelopment())
        spa.UseVueCli(npmScript: "serve");
});

app.Run();
