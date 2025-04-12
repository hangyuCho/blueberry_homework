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

    /// <summary>
    /// 이름 삭제
    /// </summary>
    /// <param name="index">이름 인덱스</param>
    void DeleteNameByIndex(int index);
}
