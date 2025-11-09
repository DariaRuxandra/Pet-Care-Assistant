using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pet_Care_Assistant.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class AppointmentListViewModel : ObservableObject
    {
        private readonly AppointmentFormViewModel _sharedVM;

        public ObservableCollection<Appointment> AllAppointments => _sharedVM.Appointments;
        public ObservableCollection<Appointment> FilteredAppointments { get; } = new();

        public ObservableCollection<string> FilterOptions { get; } = new()
        {
            "All",
            "Today",
            "Upcoming",
            "Clinic (A-Z)",
            "Pet (A-Z)"
        };

        [ObservableProperty] private string selectedFilter = "All";

        public IRelayCommand ApplyFilterCommand { get; }

        public AppointmentListViewModel(AppointmentFormViewModel sharedVM)
        {
            _sharedVM = sharedVM;
            ApplyFilterCommand = new RelayCommand(ApplyFilter);
            ApplyFilter();
            _sharedVM.Appointments.CollectionChanged += (_, __) => ApplyFilter();
        }

        private void ApplyFilter()
        {
            FilteredAppointments.Clear();
            var items = AllAppointments.AsEnumerable();

            switch (SelectedFilter)
            {
                case "Today":
                    items = items.Where(a => a.Date.Date == DateTime.Today);
                    break;
                case "Upcoming":
                    items = items.Where(a => a.Date.Date > DateTime.Today);
                    break;
                case "Clinic (A-Z)":
                    items = items.OrderBy(a => a.ClinicName);
                    break;
                case "Pet (A-Z)":
                    items = items.OrderBy(a => a.PetName);
                    break;
                default:
                    items = items.OrderBy(a => a.Date);
                    break;
            }

            foreach (var appointment in items)
                FilteredAppointments.Add(appointment);
        }
    }
}
