using System.Linq.Expressions;
using BlueberryHomeworkApp.Domain.Specification;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.Login;

public class UserByEmailSpecification(string email) : ISpecification<Domain.Entities.User>
{
    public Expression<Func<Domain.Entities.User, bool>> ToExpression()
    {
        return user => user.Email == email;
    }
}