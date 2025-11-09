using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input; // NECESAR PENTRU [RelayCommand]
using Microsoft.Maui.Controls;
using Microsoft.Maui.Storage; // NECESAR PENTRU PREFERENCES

namespace Pet_Care_Assistant.ViewModels
{
    public partial class SettingsViewModel : ObservableObject
    {
        // Constructorul: Rămâne gol.
        public SettingsViewModel()
        {
        }

        // =======================================================
        // 1. PROPRIETĂȚI GENERATE (ObservableProperty)
        // =======================================================

        // Proprietate pentru Modul Întunecat (Switch)
        [ObservableProperty]
        private bool isDarkModeEnabled;

        // Proprietate pentru Dimensiunea Textului (Slider)
        [ObservableProperty]
        private double fontSizeScale = 1.0;

        // =======================================================
        // 2. COMANDĂ PENTRU ÎNCĂRCAREA DATELOR
        // =======================================================

        [RelayCommand]
        private void LoadSettings()
        {
            // Citim și scriem direct în proprietățile publice (cu literă mare)
            IsDarkModeEnabled = Preferences.Get(nameof(IsDarkModeEnabled), false);
            FontSizeScale = Preferences.Get(nameof(FontSizeScale), 1.0);

            // Aplicăm tema imediat după încărcarea valorii
            ApplyDarkMode(IsDarkModeEnabled);

            // NOU: Aplicăm scala de font imediat după încărcarea valorii
            ApplyFontSizeScale(FontSizeScale);
        }

        // =======================================================
        // 3. LOGICA DE SCHIMBARE (On...Changed)
        // =======================================================

        partial void OnIsDarkModeEnabledChanged(bool value)
        {
            Preferences.Set(nameof(IsDarkModeEnabled), value);
            ApplyDarkMode(value);
        }

        // MODIFICARE CRUCIALĂ: Calculează și aplică scala globală
        partial void OnFontSizeScaleChanged(double value)
        {
            Preferences.Set(nameof(FontSizeScale), value);
            ApplyFontSizeScale(value);
        }
        // =======================================================
        // 4. METODE AUXILIARE
        // =======================================================

        private void ApplyFontSizeScale(double scale)
        {
            if (Application.Current != null)
            {
                // Calculează dimensiunea reală (ex: 18px * 1.2)
                const double baseSize = 18.0;
                double scaledSize = baseSize * scale;

                // Actualizează Resursa Dinamică globală, forțând UI-ul să se actualizeze
                Application.Current.Resources["GlobalFontSizeScale"] = scaledSize;
            }
        }

        private void ApplyDarkMode(bool isDark)
        {
            if (Application.Current != null)
            {
                Application.Current.UserAppTheme = isDark
                    ? AppTheme.Dark
                    : AppTheme.Light;
            }
        }
    }
}