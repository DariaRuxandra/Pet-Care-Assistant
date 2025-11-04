using Pet_Care_Assistant.Models;
using Microsoft.Maui.Controls;
using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views;

public partial class PetListPage : ContentPage
{


	public PetListPage(PetFormViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
		//BindingContext = new PetFormViewModel();

        var picker = this.FindByName<Picker>("BreedPicker");
        if (picker != null)
        {
            picker.ItemsSource = vm.DogBreeds;
            picker.ItemDisplayBinding = new Binding("Breed");
            picker.SelectedIndexChanged += BreedPicker_SelectedIndexChanged;
        }
	}

	private void BreedPicker_SelectedIndexChanged(object? sender, System.EventArgs e)
	{
        var picker = sender as Picker ?? this.FindByName<Picker>("BreedPicker");
        var label = this.FindByName<Label>("SelectedBreedLabel");

		if (picker?.SelectedItem is DogBreed selected && label != null)
		{
			label.Text = $"Selected: {selected.Breed} — {selected.Origin}";
		}
	}

	private void BreedsCollection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
        var picker = this.FindByName<Picker>("BreedPicker");
        var label = this.FindByName<Label>("SelectedBreedLabel");

		if (e.CurrentSelection?.FirstOrDefault() is DogBreed selected)
		{
			if (label != null) label.Text = $"Selected: {selected.Breed} — {selected.Origin}";
            if (picker != null) picker.SelectedItem = selected;
		}
	}
}