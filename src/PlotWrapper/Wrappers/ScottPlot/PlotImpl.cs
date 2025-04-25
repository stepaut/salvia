using PlotWrapper.Interfaces;
using PlotWrapper.Models;
using ScottPlot;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PlotWrapper.Wrappers.ScottPlot;

internal class PlotImpl : IEditablePlot
{
    private readonly Plot _plot;

    private const double DISTANCE_BETWEEN_BARS = 0.1;


    public PlotImpl()
    {
        _plot = new Plot();
    }


    public void AddScatter(double[] x, SeriesInfo y, LineSettings lineSettings)
    {
        var series = _plot.Add.Scatter(x, y.Values, y.Color.GetScottPlotColor());


        if (!lineSettings.ShowPoints)
        {
            series.MarkerSize = 0;
        }

        series.SmoothTension = lineSettings.SmoothLevel;
        series.Smooth = lineSettings.Smooth;
        series.LineWidth = (float)lineSettings.LineWidth;

        series.LegendText = y.Name;
    }

    public void AddBars(double[] x, SeriesInfo y)
    {
        var series = _plot.Add.Bars(x, y.Values);
        series.Color = y.Color.GetScottPlotColor();
        series.LegendText = y.Name;
        series.SetBorderColor(y.Color.GetScottPlotColor());

        //var autoWidth = series.Bars[0].Size;
        var autoDistance = series.Bars[1].Position - series.Bars[0].Position;
        var space = autoDistance * DISTANCE_BETWEEN_BARS;
        var barWidth = autoDistance - space;
        series.SetSize(barWidth);
    }

    public void AddStackedBars(double[] x, IList<SeriesInfo> ys)
    {
        var values = new double[ys.Count][];

        for (int i = 0; i < ys.Count; i++)
        {
            double[] bottom;
            if (i == 0)
            {
                bottom = new double[x.Length];
            }
            else
            {
                bottom = values[i - 1];
            }

            var newValues = new double[x.Length];
            for (int j = 0; j < x.Length; j++)
            {
                newValues[j] = bottom[j] + ys[i].Values[j];
            }

            values[i] = newValues;
        }

        for (int i = ys.Count - 1; i >= 0; i--)
        {
            var barPlot = _plot.Add.Bars(x, values[i]);
            barPlot.Color = ys[i].Color.GetScottPlotColor();
            barPlot.LegendText = ys[i].Name;
            barPlot.SetSize(20 * barPlot.Bars.First().Size); // magic
            barPlot.SetBorderColor(ys[i].Color.GetScottPlotColor());
        }
    }

    public void AddArea(double[] x, IList<SeriesInfo> ys, bool cumulative = false, bool normalize = false)
    {
        if (normalize)
        {
            for (int j = 0; j < x.Length; j++)
            {
                double sum = 0;
                for (int i = 0; i < ys.Count; i++)
                {
                    sum += ys[i].Values[j];
                }

                for (int i = 0; i < ys.Count; i++)
                {
                    ys[i].Values[j] = ys[i].Values[j] / sum * 100;
                }
            }
        }

        if (cumulative && ys.Count > 1)
        {
            for (int i = 1; i < ys.Count; i++)
            {
                var values = ys[i].Values;
                var newValues = new double[x.Length];
                for (int j = 0; j < x.Length; j++)
                {
                    newValues[j] = values[j] + ys[i - 1].Values[j];
                }

                ys[i].Values = newValues;
            }
        }

        for (int i = 0; i < ys.Count; i++)
        {
            double[] bottom;
            if (i == 0)
            {
                bottom = new double[x.Length];
            }
            else
            {
                bottom = ys[i - 1].Values;
            }

            var fill = _plot.Add.FillY(x, bottom, ys[i].Values);
            fill.FillColor = ys[i].Color.GetScottPlotColor();
            fill.LineColor = ys[i].Color.GetScottPlotColor();
            fill.LegendText = ys[i].Name;
        }
    }

    public void AddPie(IList<SingleInfo> items, double min)
    {
        var sum = items.Sum(x => Math.Max(x.Value, 0));

        double otherValue = 0;

        var slices = new List<PieSlice>();
        foreach (var item in items)
        {
            if (item.Value < 0) continue;

            if (100 * item.Value / sum < min)
            {
                otherValue += item.Value;
                continue;
            }

            slices.Add(new PieSlice()
            {
                Value = item.Value,
                FillColor = item.Color.GetScottPlotColor(),
                Label = item.Name,
                LabelFontSize = 18,
            });
        }

        slices.Add(new PieSlice()
        {
            Value = otherValue,
            FillColor = Colors.LightGrey,
            Label = "Other",
            LabelFontSize = 18,
        });

        var pie = _plot.Add.Pie(slices);
        pie.DonutFraction = 0.5;

        _plot.ShowLegend();

        // hide unnecessary plot components
        _plot.Axes.Frameless();
        _plot.HideGrid();
    }

    public void AddManyBars(double[] x, IList<SeriesInfo> y)
    {
        var bars = new List<BarPlot>();

        foreach (var series in y)
        {
            var barPlot = _plot.Add.Bars(x, series.Values);
            barPlot.Color = series.Color.GetScottPlotColor();
            barPlot.LegendText = series.Name;
            barPlot.SetBorderColor(series.Color.GetScottPlotColor());

            bars.Add(barPlot);
        }

        var autoDistance = bars[0].Bars[1].Position - bars[0].Bars[0].Position;
        var space = autoDistance * DISTANCE_BETWEEN_BARS;
        var cellWidth = (autoDistance - space);
        var barWidth = cellWidth / y.Count;

        var offset = -cellWidth / 2; // Start from left side of the cell

        foreach (var series in bars)
        {
            series.SetSize(barWidth);
            foreach (var bar in series.Bars)
            {
                var oldPosition = bar.Position; // center of cell
                bar.Position = oldPosition + offset;
            }

            offset += barWidth;
        }
    }

    public void SetDateTimeX()
    {
        _plot.Axes.DateTimeTicksBottom();
    }

    public byte[] GetData()
    {
        return _plot.GetImageBytes(400, 300, ImageFormat.Png);
    }
}