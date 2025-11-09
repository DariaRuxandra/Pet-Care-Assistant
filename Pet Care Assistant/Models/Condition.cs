// Pet_Care_Assistant/Models/Condition.cs
using Microsoft.Maui.Graphics;
using System.ComponentModel;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace Pet_Care_Assistant.Models
{
    public class Condition : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private bool _isExpanded;

        // Proprietăți de Bază
        public string Name { get; set; }
        public string Description { get; set; }
        public string Severity { get; set; }
        public string Urgency { get; set; }
        public string AnimalType { get; set; }
        public string ImageUrl { get; set; }
        public Color SeverityColor { get; set; }
        public string DetailsContent { get; set; }
        public string RecommendedAction { get; set; }

        // Proprietatea pentru UI (controlată de TapGestureRecognizer)
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                if (_isExpanded != value)
                {
                    _isExpanded = value;
                    OnPropertyChanged(nameof(IsExpanded));
                }
            }
        }

        // Comanda pentru a schimba starea la tap
        public ICommand ToggleExpandCommand { get; private set; }

        public Condition()
        {
            // Inițializează comanda care inversează IsExpanded la fiecare apăsare
            ToggleExpandCommand = new Command(() => IsExpanded = !IsExpanded);
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}