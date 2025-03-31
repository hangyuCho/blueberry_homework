using BlueberryHomeworkApp.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace BlueberryHomeworkApp.Controllers;

[ApiController]
/// <summary>
/// 이름 관련 API 컨트롤러
/// </summary>
[Route("api/[controller]")]
public class NameController : ControllerBase
{
    private readonly INameRepository _nameRepository;

    public NameController(INameRepository nameRepository) {
        _nameRepository = nameRepository;
    }

    /// <summary>
    /// 모든 이름 조회
    /// </summary>
    /// <returns>이름 목록</returns>
    [HttpGet]
    public IActionResult GetName() {
        return Ok(_nameRepository.GetName());
    }

    /// <summary>
    /// 이름 추가
    /// </summary>
    /// <param name="name">이름</param>
    /// <returns>이름이 추가되었습니다.</returns>
    [HttpPost]
    public IActionResult CreateName([FromBody] string name) {
        _nameRepository.AddName(name);
        return Ok("이름이 추가되었습니다.");
    }
    
    
    
}