namespace LogiSphere.Application.Models;

/// <summary>
/// This is Result class, which represents the outcome of an operation. It can either be a success or a failure, and it contains an error message if it's a failure. The generic version of the Result class also contains a value if it's a success. This pattern is often used to avoid exceptions and to provide a clear way to handle errors in a functional programming style.
/// </summary>
public class Result
{
    public bool IsSuccess { get; }
    public bool IsFailure => !IsSuccess;
    public Error Error { get; }

    protected Result(bool isSuccess, Error error)
    {
        if(isSuccess && error != Error.None || !isSuccess && error == Error.None)
        {
            throw new ArgumentException("Invalid result state");
        }

        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Succeed() => new(true, Error.None);
    public static Result Failed(Error error) => new(false, error);
};

public class Result<TValue> : Result
{
    public TValue? _value;
    
    protected internal Result(TValue? value, bool isSuccess, Error error)
        : base(isSuccess, error)
    {
        _value = value;
    }

    public TValue Value => IsSuccess ? _value! : throw new InvalidOperationException("Cannot access value of a failed result");

    public static Result<TValue> Succeed(TValue value) => new(value, true, Error.None);
    public static new Result<TValue> Failed(Error error) => new(default, false, error);
}

public record Error(string Code, string message)
{
    public static Error None => new Error(string.Empty, string.Empty);
};
