using BlueberryHomeworkApp.Domain;
using BlueberryHomeworkApp.Domain.CreateName;
using BlueberryHomeworkApp.Domain.FindName;

namespace BlueberryHomeworkApp.Repositories;

public interface INameRepository {
    /// <summary>
    /// 이름 추가
    /// </summary>
    /// <param name="command"></param>
    Result<string> AddName(CreateNameCommand command);

    /// <summary>
    /// 모든 이름 조회
    /// </summary>
    /// <returns>이름 목록</returns>
    List<FindNameResult> FindName();

    /// <summary>
    /// 이름 삭제
    /// </summary>
    Result<string> DeleteName(int index);
}
