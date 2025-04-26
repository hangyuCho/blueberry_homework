using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Infrastructure.Repositories;
using IResult = BlueberryHomeworkApp.Application.IResult;

namespace BlueberryHomeworkApp.Infrastructure;

public interface IUnitOfWork
{
    IRepository<T> GetRepository<T>() where T : class;
    Task<IResult> SaveEntitiesAsync(CancellationToken cancellationToken);
}