using BlueberryHomeworkApp.Application.Usecases.Name.CreateName;
using BlueberryHomeworkApp.Domain.Entities;
using BlueberryHomeworkApp.Infrastructure;
using BlueberryHomeworkApp.Infrastructure.Repositories;
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

// UnitOfWork등록
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Repository등록
builder.Services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));

// Mediator등록
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

// Validate 의존성 주입
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CreateNameCommand>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI(); // 기본 경로: /swagger
}

app.MapControllers();

await app.RunAsync();