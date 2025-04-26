using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace BlueberryHomeworkApp.Domain.Specification;

/// <summary>
/// [DB셋] --(query)--> [스펙의 조건 Where()] --(필터링)--> [결과]
/// </summary>
/// <typeparam name="T"></typeparam>
public abstract class BaseSpecification<T> : ISpecification<T>
{
    public abstract Expression<Func<T, bool>> ToExpression();

    public Func<IQueryable<T>, IQueryable<T>> Where()
    {
        return query => query.Where(ToExpression());
    }
}