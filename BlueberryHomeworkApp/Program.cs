
using BlueberryHomeworkApp.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Repository 의존성 주입
builder.Services.AddSingleton<INameRepository, InMemoryNameRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();