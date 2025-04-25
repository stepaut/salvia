using PlotWrapper.Models;
using System.Collections.Generic;

namespace PlotWrapper.Interfaces;

public interface IEditablePlot : IPlot
{
    void AddScatter(double[] x, SeriesInfo y, LineSettings lineSettings);
    void AddBars(double[] x, SeriesInfo y);
    void AddManyBars(double[] x, IList<SeriesInfo> y);
    void AddStackedBars(double[] x, IList<SeriesInfo> ys);
    void AddArea(double[] x, IList<SeriesInfo> ys, bool cumulative = false, bool normalize = false);
    void AddPie(IList<SingleInfo> items, double min = 3);
    void SetDateTimeX();
}
