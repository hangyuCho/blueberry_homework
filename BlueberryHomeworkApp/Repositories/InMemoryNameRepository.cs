using BlueberryHomeworkApp.Domain;
using BlueberryHomeworkApp.Domain.CreateName;
using BlueberryHomeworkApp.Domain.FindName;

namespace BlueberryHomeworkApp.Repositories;

public class InMemoryNameRepository : INameRepository {
    private readonly Dictionary<int, string> _names = new();
    private int _index = 0;

    public Result<string> AddName(CreateNameCommand command) {
        if (string.IsNullOrWhiteSpace(command.Name) || command.Name.Length > 50)
        {
            return Result<string>.Error("name must be between 1 and 50 characters");
        }
        if (_names.ContainsValue(command.Name))
        {
            return Result<string>.Error("already exists");
        }

        _names.Add(_index, command.Name);
        _index++;
        return Result<string>.Ok();
    }

    public List<FindNameResult> FindName() {
        return _names.Select(x => new FindNameResult(x.Value)).ToList();
    }

    public Result<string> DeleteName(int index) {
        if (!_names.ContainsKey(index))
        {
            return Result<string>.Error("invalid index");
        }
        _names.Remove(index);
        
        var newDict = new Dictionary<int, string>();
        var newIndex = 0;
        foreach (var pair in _names.OrderBy(kvp => kvp.Key))
        {
            newDict[newIndex++] = pair.Value;
        }
        _names.Clear();
        foreach (var pair in newDict)
        {
            _names[pair.Key] = pair.Value;
        }
        _index = _names.Count;
        return Result<string>.Ok();
    }
}
