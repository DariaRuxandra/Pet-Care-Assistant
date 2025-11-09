using Pet_Care_Assistant.Views;

namespace Pet_Care_Assistant
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("SettingsPage", typeof(SettingsPage));
            Routing.RegisterRoute(nameof(AppointmentStatsPage), typeof(AppointmentStatsPage));

        }
    }
}
