using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Application.Usecases.User.CreateUser;
using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserById;
using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserByName;
using BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;
using BlueberryHomeworkApp.Application.Usecases.User.GetUserByName;
using BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> FindUser()
    {
        return (await mediator.Send(new FindAllUserQuery())).ToApiResult();
    }

    [HttpGet("{name}")]
    public async Task<IActionResult> GetUserByName(string name)
    {
        return (await mediator.Send(new GetUserByNameQuery(name))).ToApiResult();
    }

    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserByNameCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUserById(string id)
    {
        return (await mediator.Send(new DeleteUserByIdCommand(id))).ToApiResult();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteUserByName([FromBody] DeleteUserByNameCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }
}