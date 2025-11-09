// În folderul ViewModels/ServicesViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pet_Care_Assistant.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class ServicesViewModel : ObservableObject
    {
        // Lista completă, hardcodată
        private List<Service> allServices;

        // Lista afișată în CollectionView, care se schimbă
        [ObservableProperty]
        private ObservableCollection<Service> displayedServices;

        // Proprietăți legate de controalele din XAML
        [ObservableProperty]
        private string searchText;

        [ObservableProperty]
        private double maxPrice = 500; // Prețul maxim de pe slider

        [ObservableProperty]
        private int sortOptionIndex; // 0 = A-Z, 1 = Preț cresc., 2 = Preț desc.

        public ServicesViewModel()
        {
            // ✅ LISTA DE SERVICII EXTINSĂ
            allServices = new List<Service>
    {
        // 1. Consultații & Bază
        new Service { Name = "Consultatie Generala", Price = 150, Description = "Examinare clinică completă și plan de îngrijire inițial.", ImageUrl = "icon_consult.png", TreatmentPlan = "Plan: Repaus și reevaluare în 48h." },
        new Service { Name = "Vaccinare Polivalenta", Price = 200, Description = "Imunizare anuală împotriva bolilor majore.", ImageUrl = "icon_vaccin.png", TreatmentPlan = "Plan: Evitarea efortului fizic și a contactului cu alte animale timp de 7 zile." },
        new Service { Name = "Microcipare", Price = 100, Description = "Implantare microcip de identificare, înregistrare în baza de date națională.", ImageUrl = "icon_microcip.png", TreatmentPlan = "Plan: Monitorizare zonă implant." },
        
        // 2. Profilaxie & Igienă
        new Service { Name = "Detartraj cu Ultrasunete", Price = 450, Description = "Curățare profesională a dinților sub anestezie generală.", ImageUrl = "icon_dentar.png", TreatmentPlan = "Plan: Hrană moale timp de 3 zile și soluție antiseptică bucală." },
        new Service { Name = "Taiere Unghii", Price = 50, Description = "Scurtarea ghearelor la lungimea optimă.", ImageUrl = "icon_nails.png", TreatmentPlan = "Plan: Nu necesită tratament post-intervenție." },
        new Service { Name = "Tuns Terapeutic", Price = 120, Description = "Tuns specializat pentru a preveni încâlcirea și problemele de piele.", ImageUrl = "icon_tuns.png", TreatmentPlan = "Plan: Periaj zilnic pentru menținerea blănii." },
        
        // 3. Diagnostic & Laborator
        new Service { Name = "Analize Sange Complete", Price = 250, Description = "Hemogramă și biochimie de bază pentru evaluarea funcțiilor organelor.", ImageUrl = "icon_blood.png", TreatmentPlan = "Plan: Post negru de 12 ore înainte de prelevare." },
        new Service { Name = "Ecografie Abdominala", Price = 300, Description = "Examinare imagistică non-invazivă a organelor interne.", ImageUrl = "icon_echo.png", TreatmentPlan = "Plan: Golirea vezicii urinare și post alimentar." },

        // 4. Chirurgie & Urgențe
        new Service { Name = "Sterilizare (Femela)", Price = 600, Description = "Intervenție chirurgicală pentru prevenirea reproducerii.", ImageUrl = "icon_surgery.png", TreatmentPlan = "Plan: Suturi, administrare de antibiotic și control post-operator la 7 zile." },
        new Service { Name = "Tratament Intravenos", Price = 350, Description = "Administrare de soluții medicamentoase sau perfuzii de rehidratare.", ImageUrl = "icon_iv.png", TreatmentPlan = "Plan: Monitorizare constantă și evaluarea semnelor vitale." }
    };

            // Inițial, lista afișată este identică cu lista completă
            DisplayedServices = new ObservableCollection<Service>(allServices);
        }

        // Comanda care este apelată de butonul "Aplică"
        [RelayCommand]
        private void ApplyFilters()
        {
            // 1. Începe cu lista completă
            var filteredList = allServices;

            // 2. Filtrează după textul de căutare (dacă există)
            if (!string.IsNullOrWhiteSpace(SearchText))
            {
                filteredList = filteredList.Where(s => s.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                       s.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                                           .ToList();
            }

            // 3. Filtrează după prețul de pe Slider
            filteredList = filteredList.Where(s => s.Price <= MaxPrice).ToList();

            // 4. Sortează în funcție de Picker
            switch (SortOptionIndex)
            {
                case 0: // A-Z
                    filteredList = filteredList.OrderBy(s => s.Name).ToList();
                    break;
                case 1: // Preț Crescător
                    filteredList = filteredList.OrderBy(s => s.Price).ToList();
                    break;
                case 2: // Preț Descrescător
                    filteredList = filteredList.OrderByDescending(s => s.Price).ToList();
                    break;
            }

            // 5. Actualizează lista afișată pe ecran
            DisplayedServices.Clear();
            foreach (var service in filteredList)
            {
                DisplayedServices.Add(service);
            }
        }

        // Comandă pentru afișarea schemei de tratament (opțional)
        [RelayCommand]
        private async Task ShowTreatment(Service service)
        {
            if (service == null) return;
            // Afișează un pop-up simplu cu schema
            await Shell.Current.DisplayAlert(service.Name, service.TreatmentPlan, "OK");
        }
    }
}