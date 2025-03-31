namespace BlueberryHomeworkApp.Repositories;

public interface INameRepository {
    /// <summary>
    /// 이름 추가
    /// </summary>
    /// <param name="name">이름</param>
    void AddName(string name);

    /// <summary>
    /// 모든 이름 조회
    /// </summary>
    /// <returns>이름 목록</returns>
    List<string> GetName();
}
