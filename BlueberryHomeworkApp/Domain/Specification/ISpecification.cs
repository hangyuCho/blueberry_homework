using System.Linq.Expressions;

namespace BlueberryHomeworkApp.Domain.Specification;

public interface ISpecification<T>
{
    Expression<Func<T, bool>> ToExpression();
}