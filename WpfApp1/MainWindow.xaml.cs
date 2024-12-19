using ScottPlot;
using SkiaSharp;
using System.Windows;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // create a bar plot
            double[] values = { 5, 10, 7, 13, 25, 60 };
            WpfPlot1.Plot.Add.Bars(values);
            WpfPlot1.Plot.Axes.Margins(bottom: 0);

            // create a tick for each bar
            Tick[] ticks =
            {
                new(0, "First Long Title"),
                new(1, "Second Long Title"),
                new(2, "Third Long Title"),
                new(3, "Fourth Long Title"),
                new(4, "Fifth Long Title"),
                new(5, "Sixth Long Title")
            };
            WpfPlot1.Plot.Axes.Bottom.TickGenerator = new ScottPlot.TickGenerators.NumericManual(ticks);
            WpfPlot1.Plot.Axes.Bottom.TickLabelStyle.Rotation = 45;
            WpfPlot1.Plot.Axes.Bottom.TickLabelStyle.Alignment = Alignment.MiddleLeft;

            // determine the width of the largest tick label
            float largestLabelWidth = 0;
            using SKPaint paint = new();
            foreach (Tick tick in ticks)
            {
                PixelSize size = WpfPlot1.Plot.Axes.Bottom.TickLabelStyle.Measure(tick.Label, paint).Size;
                largestLabelWidth = Math.Max(largestLabelWidth, size.Width);
            }

            // ensure axis panels do not get smaller than the largest label
            WpfPlot1.Plot.Axes.Bottom.MinimumSize = largestLabelWidth;
            WpfPlot1.Plot.Axes.Right.MinimumSize = largestLabelWidth;

            WpfPlot1.Refresh();

        }
    }
}