using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Application.Usecases.Company.CreateCompany;
using BlueberryHomeworkApp.Application.Usecases.Company.GetCompanyById;
using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserById;
using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserByName;
using BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CompanyController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetName(string id)
    {
        return (await mediator.Send(new GetCompanyByIdQuery(id))).ToApiResult();
    }

    /*
    [HttpPost]
    public async Task<IActionResult> CreateName([FromBody] CreateCompanyCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }
    */
}