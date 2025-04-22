namespace BlueberryHomeworkApp.Application;

public interface IResult
{
    public bool IsError { get; }
    public IResult AsError();
    public Exception? GetError();
}

public interface IResult<out T> : IResult
{
    public T Get();
    public new IResult<T> AsError();
}