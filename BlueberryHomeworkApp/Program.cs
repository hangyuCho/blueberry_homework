using System.Reflection;
using BlueberryHomeworkApp.Domain.CreateName;
using BlueberryHomeworkApp.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Validate 의존성 주입
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateNameCommandValidator>();

// Repository 의존성 주입
builder.Services.AddSingleton<INameRepository, InMemoryNameRepository>();

    

var app = builder.Build();

app.MapControllers();

await app.RunAsync();