namespace BlueberryHomeworkApp.Domain.Exceptions;

public class DataNotFoundException(Type entityType, object id)
    : Exception($"{entityType.Name} with id: {id} was not found.");