using Pet_Care_Assistant.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Maui.Controls;

namespace Pet_Care_Assistant.Views;

public partial class PetListPage : ContentPage
{
	public PetListPage()
	{
		InitializeComponent();

		// Load breeds and populate UI
		var breeds = GetValues();

		// Populate Picker and CollectionView
		BreedPicker.ItemsSource = breeds;
		BreedPicker.ItemDisplayBinding = new Binding("Breed");

		//BreedsCollection.ItemsSource = breeds;

		// show a few lines (first 10) to keep UI small
		//if (breeds != null && breeds.Any())
		//{
		//	var lines = breeds
		//		.Take(10)
		//		.Select(b => $"{b.Breed} — {b.Origin}");
		//	DebugLabel.Text = string.Join("\n", lines);
		//}
		//else
		//{
		//	DebugLabel.Text = "No breeds loaded (or JSON parse failed)";
		//}

		// Wire selection events
		BreedPicker.SelectedIndexChanged += BreedPicker_SelectedIndexChanged;
		//BreedsCollection.SelectionChanged += BreedsCollection_SelectionChanged;
	}

	private void BreedPicker_SelectedIndexChanged(object? sender, System.EventArgs e)
	{
		if (BreedPicker.SelectedItem is DogBreed selected)
		{
			SelectedBreedLabel.Text = $"Selected: {selected.Breed} — {selected.Origin}";
			// sync CollectionView selection
			//BreedsCollection.SelectedItem = selected;
		}
	}

	private void BreedsCollection_SelectionChanged(object? sender, SelectionChangedEventArgs e)
	{
		if (e.CurrentSelection?.FirstOrDefault() is DogBreed selected)
		{
			SelectedBreedLabel.Text = $"Selected: {selected.Breed} — {selected.Origin}";
			// sync Picker selection
			BreedPicker.SelectedItem = selected;
		}
	}

	public List<DogBreed> GetValues()
	{
		var dogBreedService = new Services.DogBreedService();
		var breeds = dogBreedService.getDogBreed();
		return breeds;
    }
}