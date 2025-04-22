using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Domain.Exceptions;
using Microsoft.EntityFrameworkCore;
using IResult = BlueberryHomeworkApp.Application.IResult;

namespace BlueberryHomeworkApp.Infrastructure.Repositories;

public class EfRepository<T>(AppDbContext dbContext) : IRepository<T>
    where T : class
{
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();

    public Task<IResult> AddAsync(T entity)
    {
        try
        {
            _dbSet.Add(entity);
            return Task.FromResult(Result.Ok());
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Error(ex));
        }
    }

    public Task<IResult> UpdateAsync(T entity)
    {
        try
        {
            _dbSet.Update(entity);
            return Task.FromResult(Result.Ok());
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Error(ex));
        }
    }

    public async Task<IResult<T>> GetAsync<TKey>(TKey id)
    {
        try
        {
            var entity = await _dbSet.FindAsync(id);
            if (id != null)
                return entity is null
                    ? Result<T>.Error(new DataNotFoundException(typeof(T), id))
                    : Result<T>.Ok(entity);
            return Result<T>.Error(new ArgumentNullException(nameof(id)));
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Result<T>.Error(ex));
        }
    }

    public Task<IResult> DeleteAsync(T entity)
    {
        try
        {
            _dbSet.Remove(entity);
            return Task.FromResult(Result.Ok());
        }
        catch (Exception ex)
        {
            return Task.FromResult(Result.Error(ex));
        }
    }

    public async Task<IResult<List<T>>> FindAllAsync()
    {
        try
        {
            var list = await _dbSet.ToListAsync();
            return Result<List<T>>.Ok(list);
        }
        catch (Exception ex)
        {
            return await Task.FromResult(Result<List<T>>.Error(ex));
        }
    }
}