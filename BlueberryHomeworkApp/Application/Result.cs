using BlueberryHomeworkApp.Application.Usecases.Name.FindAllName;

namespace BlueberryHomeworkApp.Application;

public class Result<T> : IResult<T>
{
    private Result(T data)
    {
        Data = data;
    }

    private Result(Exception exception)
    {
        Exception = exception;
    }

    private T? Data { get; }
    private Exception? Exception { get; }

    public bool IsError => Exception != null;
    public Exception? GetError() => Exception;
    public T? Get() => Data;

    IResult IResult.AsError()
    {
        if (!IsError)
        {
            throw new InvalidOperationException("result is not error state.");
        }
        else
        {
            return this;
        }
    }

    public static Result<T> Ok(T data)
    {
        return new Result<T>(data);
    }

    T IResult<T>.Get() => Data ?? throw new InvalidOperationException();

    public static Result<T> Error(Exception ex)
    {
        return new Result<T>(ex);
    }

    public IResult<T> AsError()
    {
        if (!IsError)
            throw new InvalidOperationException("result is not error state.");
        return this;
    }
}

public class Result : IResult
{
    private Result(Exception? error)
    {
        Exception = error;
    }

    private Exception? Exception { get; }

    public bool IsError => Exception != null;

    public Exception? GetError() => Exception;

    public static IResult Ok()
    {
        return new Result(null);
    }

    public static IResult Error(Exception error)
    {
        return new Result(error);
    }

    public IResult AsError()
    {
        if (!IsError)
            throw new InvalidOperationException("result is not error state.");
        return this;
    }
}