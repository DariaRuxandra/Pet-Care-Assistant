// Pet_Care_Assistant/ViewModels/HealthCheckerViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Graphics;

// Rezolvă CS0104 (Ambiguitate Condition)
using AppCondition = Pet_Care_Assistant.Models.Condition;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class HealthCheckerViewModel : ObservableObject
    {
        private List<AppCondition> allConditions;

        [ObservableProperty] private ObservableCollection<AppCondition> displayedConditions;
        [ObservableProperty] private string searchText;
        [ObservableProperty] private int severityFilterIndex;
        [ObservableProperty] private string selectedAnimalType = "Câine";

        public HealthCheckerViewModel()
        {
            DisplayedConditions = new ObservableCollection<AppCondition>();
            LoadConditions();
            ApplyFilters();
        }

        [RelayCommand]
        private void ApplyFilters()
        {
            var filteredList = allConditions.AsEnumerable();

            // 1. FILTRARE TEXT
            if (!string.IsNullOrWhiteSpace(SearchText))
                filteredList = filteredList.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) || c.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            // 2. FILTRARE GRAVITATE
            if (SeverityFilterIndex > 0)
            {
                string selectedSeverity = "";
                switch (SeverityFilterIndex)
                {
                    case 1: selectedSeverity = "Ușoară"; break;
                    case 2: selectedSeverity = "Moderată"; break;
                    case 3: selectedSeverity = "Urgență"; break;
                }
                filteredList = filteredList.Where(c => c.Severity == selectedSeverity);
            }

            // 3. FILTRARE TIP ANIMAL
            if (!string.IsNullOrWhiteSpace(SelectedAnimalType))
                filteredList = filteredList.Where(c => c.AnimalType.Contains(SelectedAnimalType));

            DisplayedConditions.Clear();
            foreach (var condition in filteredList)
            {
                DisplayedConditions.Add(condition);
            }
        }

        partial void OnSearchTextChanged(string value) => ApplyFiltersCommand.Execute(null);
        partial void OnSeverityFilterIndexChanged(int value) => ApplyFiltersCommand.Execute(null);

        private void LoadConditions()
        {
            // LISTA CU EXACT 3 AFECȚIUNI PE CATEGORIE
            allConditions = new List<AppCondition>
            {
                // --- 🚨 URGENȚĂ (3 Afecțiuni) ---
                new AppCondition
                {
                    Name = "Vomă/Diaree cu sânge",
                    Description = "Eliminare de sânge în vomă sau scaun, letargie, febră, stare de șoc.",
                    Severity = "Urgență",
                    Urgency = "Imediat la Urgențe",
                    AnimalType = "Câine, Pisică",
                    ImageUrl = "icon_vomiting.png",
                    SeverityColor = Color.FromHex("#D32F2F"), // Roșu Intens
                    DetailsContent = "Poate indica Parvoviroză, ingestia de corp străin tăios sau o intoxicație severă (ex: raticide). **Nu administrați medicamente umane.**",
                    RecommendedAction = "Transport urgent la clinică pentru analize și intervenție."
                },
                new AppCondition
                {
                    Name = "Dificultăți de respirație",
                    Description = "Gâfâit continuu, respirație pe gură (la pisici), buze/gingii albăstrui sau palide.",
                    Severity = "Urgență",
                    Urgency = "Urgență Majoră",
                    AnimalType = "Câine, Pisică",
                    ImageUrl = "icon_cough.png",
                    SeverityColor = Color.FromHex("#D32F2F"),
                    DetailsContent = "Indică insuficiență cardiacă, edem pulmonar sau obstrucție. Păstrați animalul calm și evitați stresul. Fiecare secundă contează.",
                    RecommendedAction = "Mergeți la cea mai apropiată clinică de urgență."
                },
                new AppCondition
                {
                    Name = "Colaps/Convulsii",
                    Description = "Pierderea bruscă a cunoștinței, spasme musculare incontrolabile, salivare excesivă.",
                    Severity = "Urgență",
                    Urgency = "Urgență Majoră",
                    AnimalType = "Câine, Pisică",
                    ImageUrl = "icon_seizure.png",
                    SeverityColor = Color.FromHex("#D32F2F"),
                    DetailsContent = "Convulsiile pot fi cauzate de epilepsie, hipoglicemie, traumă sau intoxicații. Nu atingeți gura animalului. Cronometrați durata crizei.",
                    RecommendedAction = "Consultați veterinarul imediat după încetarea convulsiilor (sau în timpul lor, dacă nu se opresc)."
                },
                
                // --- ⚠️ MODERATĂ (3 Afecțiuni) ---
                new AppCondition
                {
                    Name = "Infecție de ureche",
                    Description = "Scuturare frecventă a capului, scărpinat, secreții maronii cu miros neplăcut.",
                    Severity = "Moderată",
                    Urgency = "Programare în 2-3 zile",
                    AnimalType = "Câine, Pisică",
                    ImageUrl = "icon_ear.png",
                    SeverityColor = Color.FromHex("#FF9800"), // Portocaliu
                    DetailsContent = "Infecțiile bacteriene sau fungice pot duce la otită cronică. Nu folosiți alcool sanitar. Curățarea trebuie făcută cu soluții specializate.",
                    RecommendedAction = "Programare pentru diagnostic (otoscopie) și tratament topic."
                },
                new AppCondition
                {
                    Name = "Infecție urinară (UTI)",
                    Description = "Urinare frecventă, în cantități mici, urinat în locuri neobișnuite, prezența sângelui în urină.",
                    Severity = "Moderată",
                    Urgency = "Programare în 24-48h",
                    AnimalType = "Pisică",
                    ImageUrl = "icon_kidney.png",
                    SeverityColor = Color.FromHex("#FF9800"),
                    DetailsContent = "Poate evolua spre obstrucție urinară, o urgență fatală. Este crucial să se efectueze o analiză de urină. Hidratare crescută ajută.",
                    RecommendedAction = "Vizită la veterinar pentru analiză de urină și eventual antibiotice."
                },
                new AppCondition
                {
                    Name = "Leziune ușoară labă/șchiopătat",
                    Description = "Șchiopătat ușor după o activitate, fără umflătură evidentă, durere redusă la palpare.",
                    Severity = "Moderată",
                    Urgency = "Monitorizare 24h",
                    AnimalType = "Câine",
                    ImageUrl = "icon_joint.png",
                    SeverityColor = Color.FromHex("#FF9800"),
                    DetailsContent = "Poate fi o simplă entorsă sau o iritație la pernă. Odihnă obligatorie timp de o zi. Dacă șchiopătatul persistă sau se înrăutățește, consultați medicul.",
                    RecommendedAction = "Repaus forțat și monitorizarea evoluției."
                },
                
                // --- 🟢 UȘOARĂ (3 Afecțiuni) ---
                new AppCondition
                {
                    Name = "Diaree simplă",
                    Description = "Scaune moi, dar fără sânge, animalul este vioi și mănâncă normal.",
                    Severity = "Ușoară",
                    Urgency = "Monitorizare 24-48h",
                    AnimalType = "Câine, Pisică",
                    ImageUrl = "icon_diarrhea.png",
                    SeverityColor = Color.FromHex("#4CAF50"), // Verde
                    DetailsContent = "De obicei cauzată de schimbarea dietei sau de ingestia a ceva neadecvat. Dieta blândă (orez și pui/ton în apă) ajută. Suplimente probiotice recomandate.",
                    RecommendedAction = "Dietă blândă și hidratare."
                },
                new AppCondition
                {
                    Name = "Ochi umezi/Lăcrimare",
                    Description = "Lăcrimare excesivă, fără roșeață, puroi sau sensibilitate la lumină.",
                    Severity = "Ușoară",
                    Urgency = "Igienă zilnică",
                    AnimalType = "Câine, Pisică",
                    ImageUrl = "icon_eye.png",
                    SeverityColor = Color.FromHex("#4CAF50"),
                    DetailsContent = "Frecventă la rasele cu fața plată. Curățați zilnic cu soluție salină sau soluții speciale pentru ochi. Dacă apare roșeață, vizitați medicul.",
                    RecommendedAction = "Igienă locală consecventă."
                },
                new AppCondition
                {
                    Name = "Mătreață/Piele uscată",
                    Description = "Prezența mătreții, blană ternă sau ușor uleioasă. Fără mâncărime intensă.",
                    Severity = "Ușoară",
                    Urgency = "Ajustare dietă",
                    AnimalType = "Câine, Pisică",
                    ImageUrl = "icon_skin.png",
                    SeverityColor = Color.FromHex("#4CAF50"),
                    DetailsContent = "Poate fi un semn de deshidratare sau lipsă de acizi grași Omega 3/6 din dietă. Adăugați ulei de somon sau un supliment de calitate în hrană.",
                    RecommendedAction = "Suplimente nutritive și îmbunătățirea calității hranei."
                }
            };
        }
    }
}