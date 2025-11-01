using Pet_Care_Assistant.Models;
using Microsoft.Maui.Controls;
using Pet_Care_Assistant.ViewModels;

namespace Pet_Care_Assistant.Views;

public partial class PetListPage : ContentPage
{
	public PetListPage(HomeViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;

        // Use the ViewModel collection (async-aware) as the Picker ItemsSource
        BreedPicker.ItemsSource = vm.DogBreeds;
		BreedPicker.ItemDisplayBinding = new Binding("Breed");

		BreedPicker.SelectedIndexChanged += BreedPicker_SelectedIndexChanged;
	}

	private void BreedPicker_SelectedIndexChanged(object? sender, System.EventArgs e)
	{
		if (BreedPicker.SelectedItem is DogBreed selected)
		{
			SelectedBreedLabel.Text = $"Selected: {selected.Breed} — {selected.Origin}";
		}
	}

	private void BreedsCollection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection?.FirstOrDefault() is DogBreed selected)
		{
			SelectedBreedLabel.Text = $"Selected: {selected.Breed} — {selected.Origin}";
			BreedPicker.SelectedItem = selected;
		}
	}
}