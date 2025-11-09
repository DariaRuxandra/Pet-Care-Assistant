using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void OnGoToPetProfileClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("PetListPage");
        }

        private async void OnGoToAppointmentsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("AppointmentsPage");
        }

        private async void OnGoToTreatmentsClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("///ServiciesPage");
        }
    }
}
