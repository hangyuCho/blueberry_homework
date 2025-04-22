using BlueberryHomeworkApp.Application;
using BlueberryHomeworkApp.Application.Usecases.Name.CreateName;
using BlueberryHomeworkApp.Application.Usecases.Name.DeleteName;
using BlueberryHomeworkApp.Application.Usecases.Name.FindAllName;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Controllers;

/// <summary>
///     이름 관리를 위한 API 컨트롤러
///     이름의 추가, 조회, 삭제 기능을 제공합니다.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class NameController(IMediator mediator) : ControllerBase
{
    /// <summary>
    ///     저장된 모든 이름을 조회합니다.
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> FindName()
    {
        return (await mediator.Send(new FindAllNameQuery())).ToApiResult();
    }

    /// <summary>
    ///     새로운 이름을 추가합니다.
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> CreateName([FromBody] CreateNameCommand command)
    {
        return (await mediator.Send(command)).ToApiResult();
    }

    /// <summary>
    ///     지정된 인덱스의 이름을 삭제합니다.
    /// </summary>
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteName(int id)
    {
        return (await mediator.Send(new DeleteNameCommand(id))).ToApiResult();
    }
}