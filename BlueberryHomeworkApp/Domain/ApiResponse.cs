namespace BlueberryHomeworkApp.Domain;
public class Result<T>
{
    public string Message { get; set; }
    public T? Data { get; set; }

    private const string SuccessMessage = "success";
    private const string ErrorMessage = "error";
    
    private Result(string message, T? data = default)
    {
        Message = message;
        Data = data;
    }

    public static Result<T> Ok(T? data = default)
    {
        return new Result<T>(SuccessMessage, data);
    }

    public static Result<T> Error(T? data = default)
    {
        return new Result<T>(ErrorMessage, data);
    }
}