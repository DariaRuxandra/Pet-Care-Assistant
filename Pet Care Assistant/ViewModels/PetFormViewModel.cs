using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Pet_Care_Assistant.Data;
using Pet_Care_Assistant.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class PetFormViewModel : ObservableObject
    {
        private readonly AppDbContext _db;

        public ObservableCollection<Pet> Pets { get; } = new();

        [ObservableProperty] private string name;
        [ObservableProperty] private string species;
        [ObservableProperty] private string breed;
        [ObservableProperty] private int age;

        public PetFormViewModel(AppDbContext db)
        {
            _db = db;
            _ = LoadPetsAsync();
        }

        public async Task LoadPetsAsync()
        {
            Pets.Clear();
            var list = await _db.Pets.ToListAsync();
            foreach (var p in list) Pets.Add(p);
        }

        [RelayCommand]
        public async Task SavePetAsync()
        {
            var pet = new Pet { Name = Name, Species = Species, Breed = Breed, Age = Age };
            _db.Pets.Add(pet);
            await _db.SaveChangesAsync();
            Pets.Add(pet);

            // clear form
            Name = Species = Breed = "";
            Age = 0;
        }
    }
}