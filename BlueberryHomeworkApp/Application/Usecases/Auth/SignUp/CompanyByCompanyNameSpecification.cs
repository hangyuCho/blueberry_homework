using System.Linq.Expressions;
using BlueberryHomeworkApp.Domain.Specification;

namespace BlueberryHomeworkApp.Application.Usecases.Auth.SignUp;

public class CompanyByCompanyNameSpecification(string companyName) : ISpecification<Domain.Entities.Company>
{
    public Expression<Func<Domain.Entities.Company, bool>> ToExpression()
    {
        return company => company.Name == companyName;
    }
}