using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pet_Care_Assistant.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class AppointmentFormViewModel : ObservableObject
    {
        [ObservableProperty] private string petName = "";
        [ObservableProperty] private string clinicName = "";
        [ObservableProperty] private DateTime date = DateTime.Today;
        [ObservableProperty] private TimeSpan time = DateTime.Now.TimeOfDay;
        [ObservableProperty] private string reason = "";
        [ObservableProperty] private bool sendReminder = false;

        // ✅ Dropdown list for available clinics
        public ObservableCollection<string> AvailableClinics { get; } = new()
        {
            "Happy Paws Vet",
            "Animal Health Center",
            "Healthy Pets Clinic",
            "Downtown Vet",
            "PetCare Plus"
        };

        // ✅ Appointment storage
        public ObservableCollection<Appointment> Appointments { get; } = new();

        public IAsyncRelayCommand SaveAppointmentCommand { get; }
        public IRelayCommand ClearFormCommand { get; }
        public IAsyncRelayCommand CancelCommand { get; }

        public AppointmentFormViewModel()
        {
            SaveAppointmentCommand = new AsyncRelayCommand(SaveAppointmentAsync);
            ClearFormCommand = new RelayCommand(ClearForm);
            CancelCommand = new AsyncRelayCommand(CancelAppointmentAsync);

            
            Appointments.Add(new Appointment
            {
                PetName = "Buddy",
                ClinicName = "Happy Paws Vet",
                Date = DateTime.Today.AddDays(2).AddHours(10),
                Reason = "Vaccination"
            });

            Appointments.Add(new Appointment
            {
                PetName = "Luna",
                ClinicName = "Animal Health Center",
                Date = DateTime.Today.AddDays(5).AddHours(15),
                Reason = "Check-up"
            });

            Appointments.Add(new Appointment
            {
                PetName = "Charlie",
                ClinicName = "Healthy Pets Clinic",
                Date = DateTime.Today.AddDays(1).AddHours(9),
                Reason = "Grooming"
            });

            Appointments.Add(new Appointment
            {
                PetName = "Bella",
                ClinicName = "Downtown Vet",
                Date = DateTime.Today.AddDays(3).AddHours(14),
                Reason = "Follow-up Exam"
            });

            Appointments.Add(new Appointment
            {
                PetName = "Max",
                ClinicName = "PetCare Plus",
                Date = DateTime.Today.AddDays(-1).AddHours(11),
                Reason = "Ear cleaning"
            });
        }

        private async Task<bool> SaveAppointmentAsync()
        {
            try
            {
                if (string.IsNullOrWhiteSpace(PetName))
                {
                    await App.Current.MainPage.DisplayAlert("Validation Error", "Please enter the pet’s name.", "OK");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(ClinicName))
                {
                    await App.Current.MainPage.DisplayAlert("Validation Error", "Please select or enter a clinic name.", "OK");
                    return false;
                }

                if (string.IsNullOrWhiteSpace(Reason))
                {
                    await App.Current.MainPage.DisplayAlert("Validation Error", "Please provide a reason for the appointment.", "OK");
                    return false;
                }

                var appointmentDateTime = Date.Date + time;

                if (appointmentDateTime < DateTime.Now)
                {
                    await App.Current.MainPage.DisplayAlert("Validation Error", "The appointment date and time cannot be in the past.", "OK");
                    return false;
                }

                var newAppointment = new Appointment
                {
                    PetName = PetName,
                    ClinicName = ClinicName,
                    Date = appointmentDateTime,
                    Reason = Reason
                };

                Appointments.Add(newAppointment);

                if (SendReminder)
                    Console.WriteLine($"Reminder scheduled for {PetName}'s appointment at {appointmentDateTime:g}");

                await App.Current.MainPage.DisplayAlert("Success", "Appointment added successfully!", "OK");
                ClearForm();
                return true;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Something went wrong:\n{ex.Message}", "OK");
                return false;
            }
        }

        private async Task CancelAppointmentAsync()
        {
            bool confirm = await App.Current.MainPage.DisplayAlert(
                "Cancel Appointment",
                "Are you sure you want to cancel the appointment?",
                "Yes", "No");

            if (confirm)
            {
                ClearForm();
                await Shell.Current.GoToAsync("//MainPage", true);
            }
        }

        private void ClearForm()
        {
            PetName = "";
            ClinicName = "";
            Date = DateTime.Today;
            Time = DateTime.Now.TimeOfDay;
            Reason = "";
            SendReminder = false;
        }
    }
}
