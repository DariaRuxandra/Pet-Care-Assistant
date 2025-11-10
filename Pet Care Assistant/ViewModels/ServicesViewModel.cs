// Located in ViewModels/ServicesViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pet_Care_Assistant.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class ServicesViewModel : ObservableObject
    {
        // Full, hardcoded list of services
        private List<Service> allServices;

        // The list displayed in the CollectionView (changes dynamically)
        [ObservableProperty]
        private ObservableCollection<Service> displayedServices;

        // Properties bound to XAML controls
        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private double maxPrice = 600; // Maximum price from the slider

        [ObservableProperty]
        private int sortOptionIndex; // 0 = A–Z, 1 = Price Ascending, 2 = Price Descending

        public ServicesViewModel()
        {
            // ✅ EXTENDED LIST OF SERVICES
            allServices = new List<Service>
            {
                // 1. Consultations & General Care
                new Service { Name = "General Consultation", Price = 150, Description = "Comprehensive clinical examination and initial care plan.", ImageUrl = "icon_consult.png", TreatmentPlan = "Plan: Rest and re-evaluation in 48 hours." },
                new Service { Name = "Polyvalent Vaccination", Price = 200, Description = "Annual immunization against major diseases.", ImageUrl = "icon_vaccin.png", TreatmentPlan = "Plan: Avoid physical exertion and contact with other animals for 7 days." },
                new Service { Name = "Microchipping", Price = 100, Description = "Implantation of an identification microchip and registration in the national database.", ImageUrl = "icon_microcip.png", TreatmentPlan = "Plan: Monitor the implantation area." },
                
                // 2. Prophylaxis & Hygiene
                new Service { Name = "Ultrasound Descaling", Price = 450, Description = "Professional dental cleaning performed under general anesthesia.", ImageUrl = "icon_dentar.png", TreatmentPlan = "Plan: Soft food for 3 days and antiseptic mouth rinse." },
                new Service { Name = "Nail Trimming", Price = 50, Description = "Trimming the claws to the optimal length.", ImageUrl = "icon_nails.png", TreatmentPlan = "Plan: No post-procedure treatment required." },
                new Service { Name = "Therapeutic Grooming", Price = 120, Description = "Specialized haircut to prevent tangling and skin problems.", ImageUrl = "icon_tuns.png", TreatmentPlan = "Plan: Daily brushing to maintain coat quality." },
                
                // 3. Diagnosis & Laboratory
                new Service { Name = "Complete Blood Analysis", Price = 250, Description = "Full hematology and biochemistry tests to assess organ function.", ImageUrl = "icon_blood.png", TreatmentPlan = "Plan: 12-hour fasting before sample collection." },
                new Service { Name = "Abdominal Ultrasound", Price = 300, Description = "Non-invasive imaging examination of internal organs.", ImageUrl = "icon_echo.png", TreatmentPlan = "Plan: Empty the bladder and avoid food before the procedure." },

                // 4. Surgery & Emergencies
                new Service { Name = "Spaying (Female)", Price = 600, Description = "Surgical procedure to prevent reproduction.", ImageUrl = "icon_surgery.png", TreatmentPlan = "Plan: Stitches, antibiotic treatment, and post-op check-up after 7 days." },
                new Service { Name = "Intravenous Treatment", Price = 350, Description = "Administration of medicinal solutions or rehydration infusions.", ImageUrl = "icon_iv.png", TreatmentPlan = "Plan: Continuous monitoring and assessment of vital signs." }
            };

            // Initially, the displayed list is identical to the full list
            DisplayedServices = new ObservableCollection<Service>(allServices);
        }

        // Command triggered by the "Apply Filter" button
        [RelayCommand]
        private void ApplyFilters()
        {
            // 1. Start from the complete list
            var filteredList = allServices;

            // 2. Filter by search text (if provided)
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredList = filteredList.Where(s => s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                       s.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                                           .ToList();
            }

            // 3. Filter by price (slider value)
            filteredList = filteredList.Where(s => s.Price <= MaxPrice).ToList();

            // 4. Sort according to Picker selection
            switch (SortOptionIndex)
            {
                case 0: // A–Z
                    filteredList = filteredList.OrderBy(s => s.Name).ToList();
                    break;
                case 1: // Price Ascending
                    filteredList = filteredList.OrderBy(s => s.Price).ToList();
                    break;
                case 2: // Price Descending
                    filteredList = filteredList.OrderByDescending(s => s.Price).ToList();
                    break;
            }

            // 5. Update the displayed list
            DisplayedServices.Clear();
            foreach (var service in filteredList)
            {
                DisplayedServices.Add(service);
            }
        }

        // Command to display treatment plan details (optional)
        [RelayCommand]
        private async Task ShowTreatment(Service service)
        {
            if (service == null) return;

            // Display a simple pop-up with the treatment plan
            await Shell.Current.DisplayAlert(service.Name, service.TreatmentPlan, "OK");
        }
    }
}
