using System.Linq.Expressions;
using BlueberryHomeworkApp.Domain.Specification;

namespace BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;

public class CompanyByIdsSpecification(List<string> companyIds) : ISpecification<Domain.Entities.Company>
{
    public Expression<Func<Domain.Entities.Company, bool>> ToExpression()
    {
        return company => companyIds.Contains(company.Id ?? "");
    }
}