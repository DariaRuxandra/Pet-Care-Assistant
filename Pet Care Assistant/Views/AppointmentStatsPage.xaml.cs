using Microcharts;
using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views
{
    public partial class AppointmentStatsPage : ContentPage
    {
        public AppointmentStatsPage(AppointmentStatsViewModel vm)
        {
            InitializeComponent();
            BindingContext = vm;
        }
    }
}
