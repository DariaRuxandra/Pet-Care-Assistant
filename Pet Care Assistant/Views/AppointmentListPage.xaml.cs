using Pet_Care_Assistant.Models;
using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views
{
    public partial class AppointmentListPage : ContentPage
    {
        private readonly AppointmentFormViewModel _sharedVM;
        private readonly AppointmentListViewModel _vm;

        public AppointmentListPage(AppointmentListViewModel vm, AppointmentFormViewModel sharedVM)
        {
            InitializeComponent();
            _sharedVM = sharedVM;
            _vm = vm;
            BindingContext = vm;
        }

        private void OnFilterChanged(object sender, EventArgs e)
        {
            _vm.ApplyFilterCommand.Execute(null);
        }

        private async void OnAppointmentSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Appointment selected)
            {
                await DisplayAlert("Appointment Details",
                    $"🐾 Pet: {selected.PetName}\n🏥 Clinic: {selected.ClinicName}\n📅 Date: {selected.Date:dd MMM yyyy, HH:mm}\n📝 Reason: {selected.Reason}",
                    "OK");
            }
        }

        private async void OnViewStatsClicked(object sender, EventArgs e)
        {
            var statsVM = new AppointmentStatsViewModel(_sharedVM.Appointments);
            var statsPage = new AppointmentStatsPage(statsVM);
            await Navigation.PushAsync(statsPage);

        }

        private async void OnAddAppointmentClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new AppointmentFormPage(_sharedVM));
        }

        private async void OnFinishAppointmentClicked(object sender, EventArgs e)
        {
            if (sender is Button button && button.BindingContext is Appointment selectedAppointment)
            {
                bool confirm = await DisplayAlert(
                    "Finish Appointment",
                    $"Are you sure you want to mark '{selectedAppointment.PetName}' as finished?",
                    "Yes", "Cancel");

                if (confirm)
                {
                    _sharedVM.Appointments.Remove(selectedAppointment);
                    await DisplayAlert("Done", "Appointment removed successfully!", "OK");
                }
            }
        }

   /*     private async void OnScrollToTopClicked(object sender, EventArgs e)
        {
            if (MainScrollView != null)
                await MainScrollView.ScrollToAsync(0, 0, true);
        }*/
    }
}
