using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views;

public partial class PetFormPage : ContentPage
{
    public PetFormPage(PetFormViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}