// Pet_Care_Assistant/ViewModels/HealthCheckerViewModel.cs
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Maui.Graphics;
using AppCondition = Pet_Care_Assistant.Models.Condition;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class HealthCheckerViewModel : ObservableObject
    {
        private List<AppCondition> allConditions;

        [ObservableProperty] private ObservableCollection<AppCondition> displayedConditions;
        [ObservableProperty] private string searchText;
        [ObservableProperty] private int severityFilterIndex;
        [ObservableProperty] private string selectedAnimalType = "Dog";

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

            // 1. TEXT FILTER
            if (!string.IsNullOrWhiteSpace(SearchText))
                filteredList = filteredList.Where(c => c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                                                       c.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

            // 2. SEVERITY FILTER
            if (SeverityFilterIndex > 0)
            {
                string selectedSeverity = "";
                switch (SeverityFilterIndex)
                {
                    case 1: selectedSeverity = "Mild"; break;
                    case 2: selectedSeverity = "Moderate"; break;
                    case 3: selectedSeverity = "Emergency"; break;
                }
                filteredList = filteredList.Where(c => c.Severity == selectedSeverity);
            }

            // 3. ANIMAL TYPE FILTER
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
            // LIST WITH EXACTLY 3 CONDITIONS PER CATEGORY
            allConditions = new List<AppCondition>
            {
                // --- 🚨 EMERGENCY (3 Conditions) ---
                new AppCondition
                {
                    Name = "Vomiting/Diarrhea with Blood",
                    Description = "Presence of blood in vomit or stool, lethargy, fever, signs of shock.",
                    Severity = "Emergency",
                    Urgency = "Immediate Emergency Care",
                    AnimalType = "Dog, Cat",
                    ImageUrl = "icon_vomiting.png",
                    SeverityColor = Color.FromHex("#D32F2F"), // Intense Red
                    DetailsContent = "May indicate parvovirus infection, ingestion of a sharp foreign object, or severe poisoning (e.g., rat poison). **Do not administer human medication.**",
                    RecommendedAction = "Urgent transport to a veterinary clinic for diagnosis and intervention."
                },
                new AppCondition
                {
                    Name = "Breathing Difficulties",
                    Description = "Continuous panting, mouth breathing (especially in cats), bluish or pale gums/lips.",
                    Severity = "Emergency",
                    Urgency = "Major Emergency",
                    AnimalType = "Dog, Cat",
                    ImageUrl = "icon_cough.png",
                    SeverityColor = Color.FromHex("#D32F2F"),
                    DetailsContent = "May indicate heart failure, pulmonary edema, or airway obstruction. Keep the animal calm and avoid stress. Every second counts.",
                    RecommendedAction = "Go to the nearest veterinary emergency clinic immediately."
                },
                new AppCondition
                {
                    Name = "Collapse/Seizures",
                    Description = "Sudden loss of consciousness, uncontrollable muscle spasms, excessive drooling.",
                    Severity = "Emergency",
                    Urgency = "Major Emergency",
                    AnimalType = "Dog, Cat",
                    ImageUrl = "icon_seizure.png",
                    SeverityColor = Color.FromHex("#D32F2F"),
                    DetailsContent = "Seizures may be caused by epilepsy, hypoglycemia, trauma, or poisoning. Do not touch the animal’s mouth. Time the duration of the episode.",
                    RecommendedAction = "Contact your veterinarian immediately after the episode ends (or during, if it does not stop)."
                },
                
                // --- ⚠️ MODERATE (3 Conditions) ---
                new AppCondition
                {
                    Name = "Ear Infection",
                    Description = "Frequent head shaking, scratching, brownish discharge with unpleasant odor.",
                    Severity = "Moderate",
                    Urgency = "Appointment in 2–3 days",
                    AnimalType = "Dog, Cat",
                    ImageUrl = "icon_ear.png",
                    SeverityColor = Color.FromHex("#FF9800"), // Orange
                    DetailsContent = "Bacterial or fungal infections can lead to chronic otitis. Do not use alcohol. Cleaning should be done only with specialized solutions.",
                    RecommendedAction = "Schedule a veterinary consultation for diagnosis (otoscopy) and topical treatment."
                },
                new AppCondition
                {
                    Name = "Urinary Tract Infection (UTI)",
                    Description = "Frequent urination in small amounts, urinating in unusual places, traces of blood in urine.",
                    Severity = "Moderate",
                    Urgency = "Appointment within 24–48h",
                    AnimalType = "Cat",
                    ImageUrl = "icon_kidney.png",
                    SeverityColor = Color.FromHex("#FF9800"),
                    DetailsContent = "May progress to urinary blockage, which can be fatal. A urine test is essential. Increased hydration helps significantly.",
                    RecommendedAction = "Veterinary visit for urine analysis and possible antibiotic treatment."
                },
                new AppCondition
                {
                    Name = "Minor Paw Injury/Limping",
                    Description = "Mild limping after activity, no visible swelling, slight pain when touched.",
                    Severity = "Moderate",
                    Urgency = "Monitor for 24h",
                    AnimalType = "Dog",
                    ImageUrl = "icon_joint.png",
                    SeverityColor = Color.FromHex("#FF9800"),
                    DetailsContent = "May indicate a mild sprain or pad irritation. Mandatory rest for one day. If limping persists or worsens, consult a vet.",
                    RecommendedAction = "Enforce rest and monitor recovery progress."
                },
                
                // --- 🟢 MILD (3 Conditions) ---
                new AppCondition
                {
                    Name = "Simple Diarrhea",
                    Description = "Soft stools but no blood, the animal remains active and eats normally.",
                    Severity = "Mild",
                    Urgency = "Monitor for 24–48h",
                    AnimalType = "Dog, Cat",
                    ImageUrl = "icon_diarrhea.png",
                    SeverityColor = Color.FromHex("#4CAF50"), // Green
                    DetailsContent = "Usually caused by a sudden change in diet or ingestion of inappropriate food. A light diet (rice and chicken/tuna in water) helps. Probiotic supplements recommended.",
                    RecommendedAction = "Provide a light diet and ensure proper hydration."
                },
                new AppCondition
                {
                    Name = "Watery Eyes/Excessive Tearing",
                    Description = "Excessive tearing without redness, pus, or light sensitivity.",
                    Severity = "Mild",
                    Urgency = "Daily Hygiene",
                    AnimalType = "Dog, Cat",
                    ImageUrl = "icon_eye.png",
                    SeverityColor = Color.FromHex("#4CAF50"),
                    DetailsContent = "Common in flat-faced breeds. Clean the area daily with saline or eye-cleaning solutions. If redness appears, consult a veterinarian.",
                    RecommendedAction = "Consistent local hygiene and monitoring."
                },
                new AppCondition
                {
                    Name = "Dandruff/Dry Skin",
                    Description = "Presence of dandruff, dull or slightly oily coat, without intense itching.",
                    Severity = "Mild",
                    Urgency = "Diet Adjustment",
                    AnimalType = "Dog, Cat",
                    ImageUrl = "icon_skin.png",
                    SeverityColor = Color.FromHex("#4CAF50"),
                    DetailsContent = "May be a sign of dehydration or a lack of Omega 3/6 fatty acids in the diet. Add salmon oil or a quality supplement to the pet’s meals.",
                    RecommendedAction = "Add nutritional supplements and improve food quality."
                }
            };
        }
    }
}
