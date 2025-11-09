//using CommunityToolkit.Mvvm.ComponentModel;
//using CommunityToolkit.Mvvm.Input;
//using Pet_Care_Assistant.Services;
//using Pet_Care_Assistant.Models;
//using SQLite;
//using System;
//using System.Collections.Generic;
//using System.Collections.ObjectModel;
//using System.Threading.Tasks;


//namespace Pet_Care_Assistant.ViewModels
//{
//    public partial class PetFormViewModel : ObservableObject
//    {
//        public Pet pet1 = new Pet(1,"Buddy", "Dog", "Golden Retriever", DateTime.Now.AddYears(-3), "Alice Smith", "0733628134");
//        public Pet pet2 = new Pet(1,"Victor", "Dog", "Bulldog", DateTime.Now.AddYears(-2), "Daria ", "0733128134");
//        public ObservableCollection<Pet> PetsList { get; } = new();

//        public string AgeDisplay => $"{Age} year{(Age == 1 ? "" : "s")} old";




//        //private readonly SqliteConnectionFactory _db;
//        //private readonly ObservableCollection<PetDto> _pets;
//        //public ObservableCollection<PetDto> Pets => _pets;
//        //public IEnumerable<PetDto> AllPets => _pets;

//        [ObservableProperty] private string name = "";
//        [ObservableProperty] private string species = "";
//        [ObservableProperty] private string breed = "";
//        [ObservableProperty] private int age;
//        [ObservableProperty] private string ageText = "0";

//        // match PetDto fields / expose generated properties
//        [ObservableProperty] private DateTime dateOfBirth = DateTime.Now;
//        [ObservableProperty] private string ownerName = "";
//        [ObservableProperty] private string ownerContact = "";

//        //// Explicit command (avoid relying only on source-generator output)
//        public IAsyncRelayCommand SavePetCommand { get; }

//        [ObservableProperty] private DogBreed? selectedBreed;
//        public ObservableCollection<DogBreed> DogBreeds { get; } = new();

//        public PetFormViewModel()
//        {
//            PetsList.Add(pet1);
//            PetsList.Add(pet2);

//            //_db = db ?? throw new ArgumentNullException(nameof(db));
//            //_pets = new ObservableCollection<PetDto>();
//            //SavePetCommand = new AsyncRelayCommand(SavePetAsync);
//            //_ = LoadPetsAsync();

//        }

//        partial void OnDateOfBirthChanged(DateTime value)
//        {
//            Age = CalculateAge(value);
//            OnPropertyChanged(nameof(AgeDisplay)); // notify UI
//        }

//        private int CalculateAge(DateTime birthDate)
//        {
//            var today = DateTime.Today;
//            var age = today.Year - birthDate.Year;
//            if (birthDate.Date > today.AddYears(-age)) age--; // adjust if birthday hasn't occurred yet this year
//            return Math.Max(0, age);
//        }

//        partial void OnAgeTextChanged(string value)
//        {
//            if (int.TryParse(value, out var parsed))
//            {
//                Age = parsed;
//            }
//        }

//        partial void OnAgeChanged(int value)
//        {
//            AgeText = value.ToString();
//        }

//        private void SavePet()
//        {
//            if (string.IsNullOrWhiteSpace(Name) || selectedBreed == null)
//            {
//                App.Current.MainPage.DisplayAlert("Error", "Please enter a name and select a breed.", "OK");
//                return;
//            }

//            var newPet = new Pet(
//                id: 0,
//                name: Name,
//                species: "Dog", // Hardcoded since you removed Species field
//                breed: selectedBreed.Breed,
//                dateOfBirth: DateOfBirth,
//                ownerName: OwnerName,
//                ownerContact: OwnerContact
//            );

//            PetsList.Add(newPet);

//            // Optionally clear form after saving
//            Name = OwnerName = OwnerContact = "";
//            DateOfBirth = DateTime.Now;
//            selectedBreed = null;

//            App.Current.MainPage.DisplayAlert("Success", $"{newPet.Name} was added!", "OK");
//        }

//        //public async Task LoadPetsAsync()
//        //{
//        //    var conn = _db.CreateConnection();
//        //    var petDtos = await conn.Table<PetDto>().ToListAsync();
//        //    _pets.Clear();
//        //    foreach (var dto in petDtos)
//        //    {
//        //        _pets.Add(dto);
//        //    }
//        //}

//        //public async Task SavePetAsync()
//        //{
//        //    var conn = _db.CreateConnection();

//        //    var dto = new PetDto
//        //    {
//        //        Name = Name,
//        //        Species = Species,
//        //        Breed = Breed,
//        //        Age = Age,
//        //        dateOfBirth = DateOfBirth,
//        //        OwnerName = OwnerName,
//        //        OwnerContact = OwnerContact
//        //    };

//        //    await conn.InsertAsync(dto); // dto.Id will be set after insert
//        //    _pets.Add(dto);

//        //    // clear form
//        //    Name = Species = Breed = "";
//        //    Age = 0;
//        //    DateOfBirth = DateTime.Now;
//        //    OwnerName = OwnerContact = "";
//        //}
//    }
//}



using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pet_Care_Assistant.Services;
using Pet_Care_Assistant.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class PetFormViewModel : ObservableObject
    {
        private readonly DogBreedService _dogBreedService = new();

        // Form fields (auto INotifyPropertyChanged)
        [ObservableProperty] private string name = "";
        [ObservableProperty] private DateTime dateOfBirth = DateTime.Now;
        [ObservableProperty] private string ownerName = "";
        [ObservableProperty] private string ownerContact = "";
        [ObservableProperty] private DogBreed? selectedBreed;
        [ObservableProperty] private int age;
        [ObservableProperty] private string petPhotoPath = "";

        // For displaying age in UI
        public string AgeDisplay => $"{Age} year{(Age == 1 ? "" : "s")} old";

        // Dog breeds source for Picker
        public ObservableCollection<DogBreed> DogBreeds { get; } = new();

        // Saved pets
        public ObservableCollection<Pet> PetsList { get; } = new();

        public IRelayCommand SavePetCommand { get; }
        public IAsyncRelayCommand PickPhotoCommand { get; }

        public PetFormViewModel()
        {
            SavePetCommand = new RelayCommand(SavePet);
            PickPhotoCommand = new AsyncRelayCommand(PickPhotoAsync);

            // Load stored pets for testing
            Pet pet1 = new Pet(1, "Buddy", "Dog", "Golden Retriever", DateTime.Now.AddYears(-3), "Alice Smith", "0733628134");
            pet1.PhotoPath = "C:\\Users\\daria\\source\\repos\\Pet Care Assistant\\Pet Care Assistant\\Images\\dog1.jpg";
            PetsList.Add(pet1);
            PetsList.Add(new Pet(2, "Aki", "Dog", "Bulldog", DateTime.Now.AddYears(-2), "Daria", "0733128134"));

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

