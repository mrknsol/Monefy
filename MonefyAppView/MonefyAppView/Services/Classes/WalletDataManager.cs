using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;

namespace MonefyAppView.Services.Classes
{
    internal class WalletDataManager
    {
        public Dictionary<Color, double> ChartValues = new Dictionary<Color, double>();
        public Dictionary<DateTime, Dictionary<Color, double>> chartValuesByDate = new Dictionary<DateTime, Dictionary<Color, double>>();


        public bool HasChartForDate(DateTime date)
        {
            return chartValuesByDate.ContainsKey(date);
        }



        public void UpdateChart(PieChart chart, Button _button)
        {
            chart.Series.Clear();
            foreach (var kvp in ChartValues)
            {
                chart.Series.Add(new PieSeries()
                {
                    Values = new ChartValues<double> { kvp.Value },
                    Fill = new SolidColorBrush(kvp.Key),
                    Title = _button.Name
                }) ;
            }
        }

        public void UpdateChartForCurrentDate(ref DateTime currentDate, PieChart chart, Button _button)
        {

            if (chartValuesByDate.ContainsKey(currentDate))
            {
                ChartValues = new Dictionary<Color, double>(chartValuesByDate[currentDate]);
                UpdateChart(chart, _button);
            }
            else
            {
                ChartValues.Clear();
                UpdateChart(chart, _button);
            }

        }

        public void SaveChartValuesForDate(DateTime date)
        {
            chartValuesByDate[date] = new Dictionary<Color, double>(ChartValues);
        }
    }
}
