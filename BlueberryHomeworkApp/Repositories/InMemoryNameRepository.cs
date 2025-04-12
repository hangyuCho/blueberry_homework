namespace BlueberryHomeworkApp.Repositories;

public class InMemoryNameRepository : INameRepository {
    private readonly List<string> _names = new();

    public void AddName(string name) {
        _names.Add(name);
    }

    public List<string> GetName() {
        return _names;
    }

    public void DeleteNameByIndex(int index) {
        if (index < 0 || index >= _names.Count)
        {
            throw new ArgumentException("유효하지 않은 인덱스입니다.");
        }
        _names.RemoveAt(index);
    }
}
