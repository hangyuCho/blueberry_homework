using BlueberryHomeworkApp.Middleware;
using BlueberryHomeworkApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Repository 의존성 주입
builder.Services.AddSingleton<INameRepository, InMemoryNameRepository>();

var app = builder.Build();

// 예외 처리 미들웨어 등록
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();