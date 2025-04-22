namespace BlueberryHomeworkApp.Domain.Exceptions;

public class ConflictException(string field, string value)
    : Exception($"{value} is already exists for field: {field}.");