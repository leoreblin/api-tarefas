using Microsoft.EntityFrameworkCore;
using Thunders.Tasks.Application;
using Thunders.Tasks.Core.Repositories;
using Thunders.Tasks.Infrastructure.Persistence;
using Thunders.Tasks.Infrastructure.Persistence.Repositories;
using Thunders.Tasks.WebApi.Extensions;
using Thunders.Tasks.WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

// Add DbContext
builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add Repositories
builder.Services.AddTransient<ITarefaRepository, TarefaRepository>();

// Add MediatR
builder.Services.AddMediatR(configuration =>
    configuration.RegisterServicesFromAssembly(ApplicationAssembly.Assembly));

// Add Middlewares
builder.Services.AddTransient<GlobalExceptionHandlingMiddleware>();

var app = builder.Build();

app.UseHttpsRedirection();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ApplyMigrations();
}

app.UseMiddleware<GlobalExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();