using System.Linq.Expressions;
using BlueberryHomeworkApp.Domain.Specification;

namespace BlueberryHomeworkApp.Application.Usecases.Company.GetCompanyById;

public class UserByCompanyIdSpecification(string companyId) : ISpecification<Domain.Entities.User>
{
    public Expression<Func<Domain.Entities.User, bool>> ToExpression()
    {
        return user => user.CompanyId == companyId;
    }
}