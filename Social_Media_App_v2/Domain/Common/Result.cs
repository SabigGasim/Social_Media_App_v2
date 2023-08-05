namespace Domain.Common;
public class Result
{
    public required bool Success { get; init; }
    public IEnumerable<Exception>? Errors { get; init; }
}

public class Result<T> : Result
{
    public required T? Value { get; init; }
}

public static class Results
{
    private static Result successResult = new Result { Success = true };

    public static Result Fail(List<Exception>? errors) => new Result { Success = false, Errors = errors };
    public static Result Fail(Exception error) => new Result { Success = false, Errors = new List<Exception> { error } };
    public static Result Success() => successResult;
    
    public static Result<T> Success<T>(T value) => new Result<T> { Success = true, Value = value };
    public static Result<T> Fail<T>(Exception? error) => 
        new Result<T> { Success = true, Value = default, Errors = error is null ? null : new List<Exception> { error } };
    public static Result<T> Fail<T>(List<Exception>? errors) => 
        new Result<T> { Success = true, Value = default, Errors = errors is null ? null : errors };
}