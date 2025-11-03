using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Pet_Care_Assistant.Services;
using Pet_Care_Assistant.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class PetFormViewModel : ObservableObject
    {
        private readonly SqliteConnectionFactory _db;
        private readonly ObservableCollection<PetDto> _pets;
        public ObservableCollection<PetDto> Pets => _pets;
        public IEnumerable<PetDto> AllPets => _pets;

        [ObservableProperty] private string name = "";
        [ObservableProperty] private string species = "";
        [ObservableProperty] private string breed = "";
        [ObservableProperty] private int age;
        [ObservableProperty] private string ageText = "0";

        // match PetDto fields / expose generated properties
        [ObservableProperty] private DateTime dateOfBirth = DateTime.Now;
        [ObservableProperty] private string ownerName = "";
        [ObservableProperty] private string ownerContact = "";

        // Explicit command (avoid relying only on source-generator output)
        public IAsyncRelayCommand SavePetCommand { get; }

        public PetFormViewModel(SqliteConnectionFactory db)
        {
            _db = db ?? throw new ArgumentNullException(nameof(db));
            _pets = new ObservableCollection<PetDto>();
            SavePetCommand = new AsyncRelayCommand(SavePetAsync);
            _ = LoadPetsAsync();
        }

        // Keep AgeText and Age in sync
        partial void OnAgeTextChanged(string value)
        {
            if (int.TryParse(value, out var parsed))
            {
                Age = parsed;
            }
        }

        partial void OnAgeChanged(int value)
        {
            AgeText = value.ToString();
        }

        public async Task LoadPetsAsync()
        {
            var conn = _db.CreateConnection();
            var petDtos = await conn.Table<PetDto>().ToListAsync();
            _pets.Clear();
            foreach (var dto in petDtos)
            {
                _pets.Add(dto);
            }
        }

        public async Task SavePetAsync()
        {
            var conn = _db.CreateConnection();

            var dto = new PetDto
            {
                Name = Name,
                Species = Species,
                Breed = Breed,
                Age = Age,
                dateOfBirth = DateOfBirth,
                OwnerName = OwnerName,
                OwnerContact = OwnerContact
            };

            await conn.InsertAsync(dto); // dto.Id will be set after insert
            _pets.Add(dto);

            // clear form
            Name = Species = Breed = "";
            Age = 0;
            DateOfBirth = DateTime.Now;
            OwnerName = OwnerContact = "";
        }
    }
}