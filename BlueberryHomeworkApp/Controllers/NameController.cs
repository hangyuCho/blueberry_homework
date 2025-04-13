using BlueberryHomeworkApp.Domain;
using BlueberryHomeworkApp.Domain.CreateName;
using BlueberryHomeworkApp.Domain.FindName;
using BlueberryHomeworkApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Controllers;

/// <summary>
/// 이름 관리를 위한 API 컨트롤러
/// 이름의 추가, 조회, 삭제 기능을 제공합니다.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class NameController : ControllerBase
{
    private readonly INameRepository _nameRepository;

    /// <summary>
    /// 컨트롤러 생성자
    /// </summary>
    /// <param name="nameRepository">이름 저장소 인스턴스</param>
    public NameController(INameRepository nameRepository) {
        _nameRepository = nameRepository;
    }

    /// <summary>
    /// 저장된 모든 이름을 조회합니다.
    /// </summary>
    [HttpGet]
    public IActionResult FindName() {
        return Ok(Result<List<FindNameResult>>.Ok(_nameRepository.FindName()));
    }

    /// <summary>
    /// 새로운 이름을 추가합니다.
    /// </summary>
    [HttpPost]
    public IActionResult CreateName([FromBody] CreateNameCommand command) {
        return Ok(_nameRepository.AddName(command));
    }

    /// <summary>
    /// 지정된 인덱스의 이름을 삭제합니다.
    /// </summary>
    [HttpDelete("{index}")]
    public IActionResult DeleteName(int index) {
        return Ok(_nameRepository.DeleteName(index));
    }
}