namespace salvia.Core.Temperature;

public class AddingTemperatureResponse
{
    public required bool Success { get; init; }
    public string? ErrorMessage { get; init; } = null;
    public bool DiseasesCreated { get; init; } = false;
}
