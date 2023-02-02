using Microsoft.EntityFrameworkCore;
using PostService.Data;
using PostService.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "PostService", Version = "v1" });
});

// retrieve connection string
var connectionString = builder.Configuration.GetConnectionString("LocalDbConnection");

// Add DbContext
builder.Services.AddDbContext<PostServiceContext>(options =>
options.UseSqlServer(connectionString));

var scope = builder.Services.BuildServiceProvider().CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<PostServiceContext>();
dbContext.Database.EnsureCreated();

// only one instance of the PostServiceContext class throughout the lifetime of the application
// and this instance will be shared between different parts of the application.
//builder.Services.AddSingleton(dbContext);

// Listen for integration events
Subscribe.ListenForIntegrationEvents(dbContext);

// Build the application
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseAuthorization();

app.MapControllers();

app.Run();
