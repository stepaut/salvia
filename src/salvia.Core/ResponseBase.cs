namespace salvia.Core;

public class ResponseBase
{
    public required bool Success { get; init; }
    public string? ErrorMessage { get; init; } = null;
}
