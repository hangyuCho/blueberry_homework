using BlueberryHomeworkApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Controllers;

/// <summary>
/// 이름 관리를 위한 API 컨트롤러
/// 이름의 추가, 조회, 삭제 기능을 제공합니다.
/// </summary>
[ApiController]
/// <summary>
/// 이름 관련 API 컨트롤러
/// </summary>
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
    /// <returns>이름 목록을 반환합니다.</returns>
    /// <response code="200">이름 목록 조회 성공</response>
    [HttpGet]
    public IActionResult GetName() {
        return Ok(_nameRepository.GetName());
    }

    /// <summary>
    /// 새로운 이름을 추가합니다.
    /// </summary>
    /// <param name="name">추가할 이름</param>
    /// <returns>추가 성공 메시지</returns>
    /// <response code="200">이름 추가 성공</response>
    [HttpPost]
    public IActionResult CreateName([FromBody] string name) {
        _nameRepository.AddName(name);
        return Ok("이름이 추가되었습니다.");
    }

    /// <summary>
    /// 지정된 인덱스의 이름을 삭제합니다.
    /// </summary>
    /// <param name="index">삭제할 이름의 인덱스</param>
    /// <returns>삭제 성공 메시지</returns>
    /// <response code="200">이름 삭제 성공</response>
    /// <response code="400">유효하지 않은 인덱스</response>
    [HttpDelete("{index}")]
    public IActionResult DeleteNameByIndex(int index) {
        _nameRepository.DeleteNameByIndex(index);
        return Ok("이름이 삭제되었습니다.");
    }
}