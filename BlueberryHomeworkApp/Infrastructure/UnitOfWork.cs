using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Infrastructure.Repositories;
using IResult = BlueberryHomeworkApp.Application.IResult;

namespace BlueberryHomeworkApp.Infrastructure;

public class UnitOfWork(AppDbContext context) : IUnitOfWork
{
    public IRepository<T> GetRepository<T>() where T : class
    {
        return new EfRepository<T>(context);
    }

    public async Task<IResult> CommitAsync(CancellationToken cancellationToken)
    {
        try
        {
            await context.SaveChangesAsync(cancellationToken);
            return Result.Ok();
        }
        catch (Exception ex)
        {
            return Result.Error(ex);
        }
    }
}