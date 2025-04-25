using System.Drawing;

namespace PlotWrapper.Models;

public record SeriesInfo
{
    public required string Name { get; init; }
    public required Color Color { get; init; }
    public required double[] Values { get; set; }
}
