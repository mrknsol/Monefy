using LiveCharts.Defaults;
using System.Windows.Controls;
using MonefyAppView.ViewModels;

namespace MonefyAppView.Views
{
    public partial class WalletData : UserControl
    {
        //public PieChart Chart { get; set; } = new();

        public WalletData()
        {
            InitializeComponent();
            DataContext = App.Container.GetInstance<WalletDataViewModel>();
        }

        //public RelayCommand<Button> AddElement
        //{
        //    get => new(button =>
        //    {
        //        SolidColorBrush btnBackground = button.Foreground as SolidColorBrush;
        //        Color btnColor = btnBackground.Color;

        //        foreach (PieSeries i in Chart.Series)
        //        {
        //            Color color = ((SolidColorBrush)i.Fill).Color;
        //            if (color == btnColor)
        //            {
        //                i.Values = new ChartValues<double> { 7 };
        //                return;
        //            }
        //        }



        //        Chart.Series.Add(new PieSeries
        //        {
        //            Values = new ChartValues<double> { 100 },
        //            Fill = new SolidColorBrush(btnColor)

        //        });
        //    }

        //    );
        //} 

    }
}
