using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

// ATENȚIE: Namespace-ul corect cu underscore:
namespace Pet_Care_Assistant.ViewModels;

public partial class AboutViewModel : ObservableObject
{
    // Proprietate legată (binded) de IsVisible a etichetei Termeni și Condiții
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(TermsButtonText))]
    private bool _isTermsVisible;

    // Proprietate legată (binded) de IsVisible a etichetei Politica de Confidențialitate
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(PrivacyButtonText))]
    private bool _isPrivacyVisible;

    // Proprietate calculată pentru a schimba textul butonului Termeni și Condiții
    public string TermsButtonText => IsTermsVisible ? "Ascunde Detaliile" : "Afișează/Ascunde Termeni și Condiții";

    // Proprietate calculată pentru a schimba textul butonului Politica de Confidențialitate
    public string PrivacyButtonText => IsPrivacyVisible ? "Ascunde Detaliile" : "Afișează/Ascunde Politica de Confidențialitate";

    public AboutViewModel()
    {
        // Constructor
    }

    // Comanda executată la apăsarea butonului "Termeni și Condiții"
    [RelayCommand]
    private void ToggleTerms()
    {
        IsTermsVisible = !IsTermsVisible;
    }

    // Comanda executată la apăsarea butonului "Politica de Confidențialitate"
    [RelayCommand]
    private void TogglePrivacy()
    {
        IsPrivacyVisible = !IsPrivacyVisible;
    }
}