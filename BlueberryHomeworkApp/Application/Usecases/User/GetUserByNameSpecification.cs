using System.Linq.Expressions;
using BlueberryHomeworkApp.Domain.Specification;

namespace BlueberryHomeworkApp.Application.Usecases.User;

public class GetUserByNameSpecification(string name) : ISpecification<Domain.Entities.User>
{
    public Expression<Func<Domain.Entities.User, bool>> ToExpression()
    {
        return user => user.Name == name;
    }
}