
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pet_Care_Assistant.Services;
using Pet_Care_Assistant.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Linq;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class PetFormViewModel : ObservableObject
    {
        private readonly DogBreedService _dogBreedService = new();

        [ObservableProperty] private string name = "";
        [ObservableProperty] private DateTime dateOfBirth = DateTime.Now;
        [ObservableProperty] private string ownerName = "";
        [ObservableProperty] private string ownerContact = "";
        [ObservableProperty] private DogBreed? selectedBreed;
        [ObservableProperty] private int age;
        [ObservableProperty] private string petPhotoPath = "";

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(FilteredPetsList))]
        private string searchText = "";

        public IEnumerable<Pet> FilteredPetsList =>
        string.IsNullOrWhiteSpace(SearchText)
            ? PetsList
            : PetsList.Where(p => p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        public string AgeDisplay => $"{Age} year{(Age == 1 ? "" : "s")} old";

        public ObservableCollection<DogBreed> DogBreeds { get; } = new();

        public ObservableCollection<Pet> PetsList { get; } = new();

        public IRelayCommand SavePetCommand { get; }
        public IAsyncRelayCommand PickPhotoCommand { get; }

        public PetFormViewModel()
        {
            SavePetCommand = new RelayCommand(SavePet);
            PickPhotoCommand = new AsyncRelayCommand(PickPhotoAsync);

            Pet pet1 = new Pet(1, "Buddy", "Dog", "Golden Retriever", DateTime.Now.AddYears(-3), "Alice Smith", "0733628134");
            pet1.PhotoPath = "golden.png";
            PetsList.Add(pet1);
            Pet pet2 = new Pet(2, "Aki", "Dog", "Bulldog", DateTime.Now.AddYears(-2), "Samantha Jones", "0733128134");
            pet2.PhotoPath = "bulldog.png";
            PetsList.Add(pet2);

            _ = LoadDogBreedsAsync();
        }

        // Dynamically compute age from date
        partial void OnDateOfBirthChanged(DateTime value)
        {
            Age = CalculateAge(value);

            OnPropertyChanged(nameof(AgeDisplay));
        }

        private int CalculateAge(DateTime birthDate)
        {
            var today = DateTime.Today;
            int result = today.Year - birthDate.Year;
            if (birthDate.Date > today.AddYears(-result)) result--;
            return Math.Max(0, result);

            
        }

        

        //partial void OnAgeChanged(int value)
        //{
        //    OnPropertyChanged(nameof(AgeDisplayDetailed));
        //}

        private async Task LoadDogBreedsAsync()
        {
            var breeds = await _dogBreedService.GetDogBreedsAsync();
            DogBreeds.Clear();

            foreach (var breed in breeds)
                DogBreeds.Add(breed);

            System.Diagnostics.Debug.WriteLine($"? Loaded {DogBreeds.Count} breeds.");
        }


        private void SavePet()
        {
            if (string.IsNullOrWhiteSpace(Name) || SelectedBreed == null)
            {
                App.Current.MainPage.DisplayAlert("Error", "Please enter a name and select a breed.", "OK");
                return;
            }

            var newPet = new Pet(
                id: PetsList.Count + 1,
                name: Name,
                species: "Dog",
                breed: SelectedBreed.Breed,
                dateOfBirth: DateOfBirth,
                ownerName: OwnerName,
                ownerContact: OwnerContact
            )
            {
                PhotoPath = petPhotoPath
            };

            PetsList.Add(newPet);

            Name = "";
            OwnerName = "";
            OwnerContact = "";
            DateOfBirth = DateTime.Now;
            SelectedBreed = null;

            App.Current.MainPage.DisplayAlert("Success", $"{newPet.Name} added!", "OK");
        }

        private async Task PickPhotoAsync()
        {
            try
            {
                var result = await FilePicker.PickAsync(new PickOptions
                {
                    PickerTitle = "Select a pet photo",
                    FileTypes = FilePickerFileType.Images
                });

                if (result != null)
                {
                
                    string imagesDir = Path.Combine(FileSystem.AppDataDirectory, "Images");
                    Directory.CreateDirectory(imagesDir);

                    string targetFile = Path.Combine(imagesDir, result.FileName);

                    using (var sourceStream = await result.OpenReadAsync())
                    using (var destinationStream = File.Create(targetFile))
                    {
                        await sourceStream.CopyToAsync(destinationStream);
                    }

                    PetPhotoPath = targetFile;
                }
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Error", $"Could not load photo: {ex.Message}", "OK");
            }
        }
    }
}

