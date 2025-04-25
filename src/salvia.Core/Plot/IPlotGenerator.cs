using PlotWrapper.Interfaces;
using salvia.Data.Dto;

namespace salvia.Core.Plot;

public interface IPlotGenerator
{
    IPlot GenerateByTemperatures(ICollection<TemperatureDto> temperatures);
}
