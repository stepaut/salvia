using PlotWrapper.Interfaces;

namespace PlotWrapper;

public static class PlotFabric
{
    public static IEditablePlot CreatePlot()
    {
        return new Wrappers.ScottPlot.PlotImpl();
    }
}
