using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Application.Usecases.User.CreateUser;
using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserById;
using BlueberryHomeworkApp.Application.Usecases.User.DeleteUserByName;
using BlueberryHomeworkApp.Application.Usecases.User.FindAllUser;
using BlueberryHomeworkApp.Application.Usecases.User.UpdateUserByName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Controllers;

/// <summary>
///     유저를 관리하기 위한 API 컨트롤러
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     저장된 모든 이름을 조회합니다.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FindName()
    {
        return (await mediator.Send(new FindAllUserQuery())).ToApiResult();
    }

    /// <summary>
    ///     새로운 유저를 추가합니다.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateName([FromBody] CreateUserCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }

    /// <summary>
    ///     유저의 이름을 갱신합니다.
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    [HttpPut]
    public async Task<IActionResult> UpdateUser([FromBody] UpdateUserByNameCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }

    /// <summary>
    ///     지정된 인덱스의 유저를 삭제합니다.
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNameById(string id)
    {
        return (await mediator.Send(new DeleteUserByIdCommand(id))).ToApiResult();
    }

    /// <summary>
    ///     지정된 이름의 유저를 삭제합니다.
    /// </summary>
    [HttpDelete]
    public async Task<IActionResult> DeleteNameByName([FromBody] DeleteUserByNameCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }
}