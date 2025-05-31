using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Domain.Specification;
using IResult = BlueberryHomeworkApp.Application.IResult;

namespace BlueberryHomeworkApp.Infrastructure.Repositories;

public interface IRepository<T> where T : class
{
    Task<IResult> AddAsync(T entity);
    Task<IResult> UpdateAsync(T entity);

    Task<IResult> DeleteAsync(T entity);

    Task<IResult<T>> GetAsync<TKey>(TKey id);
    Task<IResult<T>> GetAsync(ISpecification<T> spec);

    Task<IResult<List<T>>> FindAsync(ISpecification<T> spec);
    Task<IResult<List<T>>> FindAllAsync();
}