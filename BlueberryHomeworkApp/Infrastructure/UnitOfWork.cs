using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Infrastructure.Repositories;
using IResult = BlueberryHomeworkApp.Application.IResult;

namespace BlueberryHomeworkApp.Infrastructure;

public class UnitOfWork(MongoDbContext context) : IUnitOfWork
{
    public IRepository<T> GetRepository<T>() where T : class
    {
        //return new EfRepository<T>(context);
        return new MongoRepository<T>(context);
    }
}