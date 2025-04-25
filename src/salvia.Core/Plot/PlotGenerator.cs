using PlotWrapper;
using PlotWrapper.Interfaces;
using PlotWrapper.Models;
using salvia.Data.Dto;
using System.Drawing;

namespace salvia.Core.Plot;

internal class PlotGenerator : IPlotGenerator
{
    public IPlot GenerateByTemperatures(ICollection<TemperatureDto> temperatures)
    {
        var plot = PlotFabric.CreatePlot();

        var x = temperatures.Select(x => x.Date.ToOADate()).ToArray();
        var y = temperatures.Select(x => (double)x.Temperature).ToArray();

        var seriesInfo = new SeriesInfo() { Color = Color.Blue, Name = "Temp", Values = y };
        var lineSettings = LineSettings.Default with { Smooth = false };

        plot.AddScatter(x, seriesInfo, lineSettings);

        plot.SetDateTimeX();

        return plot;    
    }
}
