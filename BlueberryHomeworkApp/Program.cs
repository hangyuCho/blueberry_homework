using BlueberryHomeworkApp.Application.Usecases.User.CreateUserById;
using BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;
using BlueberryHomeworkApp.Infrastructure;
using BlueberryHomeworkApp.Infrastructure.Migrations;
using BlueberryHomeworkApp.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using MongoDB.Driver;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Swagger등록
builder.Services.AddSwaggerGen();

// DbContext등록
builder.Services.AddSingleton<MongoDbContext>(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
    var connectionString = configuration.GetConnectionString("MongoDbConnection"); // MongoDB 연결 문자열을 설정
    return new MongoDbContext(connectionString ?? "");
});

// UnitOfWork등록
builder.Services.AddSingleton<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IRepository<>), typeof(MongoRepository<>));

// Mediator등록
builder.Services.AddMediatR(typeof(Program).Assembly);

// Validate 의존성 주입
builder.Services
    .AddFluentValidationAutoValidation()
    .AddFluentValidationClientsideAdapters()
    .AddValidatorsFromAssemblyContaining<CreateUserValidator>()
    .AddValidatorsFromAssemblyContaining<GetUserByNameValidator>();

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

// MongoDB 연결 설정
var mongoConnectionString = builder.Configuration.GetConnectionString("MongoDbConnection")
                            ?? throw new InvalidOperationException(
                                "MongoDB 연결 문자열이 설정되지 않았습니다. appsettings.json에서 'ConnectionStrings:MongoDbConnection'을 확인해주세요.");
var mongoClient = new MongoClient(mongoConnectionString);
var databaseName = builder.Configuration.GetSection("Mongo:DatabaseName").Value
                   ?? throw new InvalidOperationException(
                       "MongoDB 데이터베이스 이름이 설정되지 않았습니다. appsettings.json에서 'Mongo:DatabaseName'을 확인해주세요.");
var database = mongoClient.GetDatabase(databaseName);

// 마이그레이션 실행
Console.WriteLine("마이그레이션을 시작합니다...");
var migrationManager = new MigrationManager(database);
await migrationManager.MigrateAsync();
Console.WriteLine("마이그레이션이 완료되었습니다.");

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowAll");
    app.UseSwagger();
    app.UseSwaggerUI(); // 기본 경로: /swagger
}

app.MapControllers();

await app.RunAsync();