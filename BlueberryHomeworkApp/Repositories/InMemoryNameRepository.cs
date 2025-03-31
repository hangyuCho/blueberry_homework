namespace BlueberryHomeworkApp.Repositories;

public class InMemoryNameRepository : INameRepository {
    private readonly List<string> _names = new();

    public void AddName(string name) {
        _names.Add(name);
    }

    public List<string> GetName() {
        return _names;
    }
}
