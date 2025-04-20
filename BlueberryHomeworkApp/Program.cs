using System.Reflection;
using BlueberryHomeworkApp.Domain.CreateName;
using BlueberryHomeworkApp.Infrastructure;
using BlueberryHomeworkApp.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger등록
builder.Services.AddSwaggerGen();

// DbContext등록
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Validate 의존성 주입
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters();

builder.Services.AddValidatorsFromAssemblyContaining<CreateNameValidator>();

// Repository 의존성 주입
builder.Services.AddSingleton<INameRepository, InMemoryNameRepository>();

    

var app = builder.Build();

app.MapControllers();

await app.RunAsync();