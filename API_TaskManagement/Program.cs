using Infrastructure.Data;
using MediatR;
using Microsoft.OpenApi.Models;
using TaskRoutine.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add ConnectionOptions (from config)
var connectionOptions = new ConnectionOptions
{
    ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
};
builder.Services.AddSingleton(connectionOptions);

// Register Repositories with options
builder.Services.AddScoped<TaskRepository>();
builder.Services.AddScoped<RoutineRepository>();

// MediatR
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(TaskRoutine.Application.Features.Tasks.Commands.CreateTaskCommand).Assembly));

// Controllers & Swagger
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "TaskRoutine API", Version = "v1" });
});


// CORS for Angular (Allow All)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI();
}
if(app.Environment.IsProduction())
{
    app.UseStaticFiles();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "TaskRoutine API V1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseCors("AllowAll");
app.MapControllers();

app.Run();