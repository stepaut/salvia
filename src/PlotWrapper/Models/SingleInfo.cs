using System.Drawing;

namespace PlotWrapper.Models;

public record SingleInfo
{
    public required string Name { get; init; }
    public required Color Color { get; init; }
    public required double Value { get; set; }
}
