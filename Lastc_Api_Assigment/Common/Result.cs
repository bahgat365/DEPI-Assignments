namespace DEPI_ECommerce_API.Common;

// Generic Result pattern: return a consistent success/failure structure from API endpoints.
public class Result<T>
{
    public bool IsSuccess { get; init; }
    public string? Message { get; init; }
    public T? Data { get; init; }
    public IReadOnlyList<string> Errors { get; init; } = Array.Empty<string>();

    public static Result<T> Success(T data, string? message = null) =>
        new() { IsSuccess = true, Data = data, Message = message };

    public static Result<T> Failure(string message, params string[] errors) =>
        new() { IsSuccess = false, Message = message, Errors = errors };
}
