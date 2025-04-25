namespace PlotWrapper.Models;

public record LineSettings
{
    public bool Smooth { get; init; } = true;
    public double SmoothLevel { get; init; } = 0.5;
    public double LineWidth { get; init; } = 2;
    public bool ShowPoints { get; init; } = true;

    public static readonly LineSettings Default = new();
}
