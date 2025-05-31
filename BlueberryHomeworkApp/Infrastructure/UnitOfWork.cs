using BlueberryHomeworkApp.Infrastructure.Repositories;

namespace BlueberryHomeworkApp.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly MongoDbContext _context;
    private readonly ILoggerFactory _loggerFactory;

    public UnitOfWork(MongoDbContext context, ILoggerFactory loggerFactory)
    {
        _context = context;
        _loggerFactory = loggerFactory;
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
        var collectionName = typeof(T).Name.ToLower(); // 예: User -> user, Company -> company
        var logger = _loggerFactory.CreateLogger<MongoRepository<T>>();
        return new MongoRepository<T>(_context, collectionName, logger);
    }
}