using CommunityToolkit.Mvvm.ComponentModel;
using Pet_Care_Assistant.Models;
using Pet_Care_Assistant.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Pet_Care_Assistant.ViewModels
{
    public partial class HomeViewModel : ObservableObject
    {
        private readonly DogBreedService _dogBreedService;
        private DogBreed _dogBreed;

        [ObservableProperty]
        private ObservableCollection<DogBreed> dogBreeds = new ObservableCollection<DogBreed>();

        public HomeViewModel(DogBreedService dogBreedService)
        {
            _dogBreedService = dogBreedService;
            _ = GetDogBreed();
        }

        private Task<List<DogBreed>> GetValues()
        {
            var breeds = _dogBreedService.getDogBreed();
            return Task.FromResult(breeds);
        }

        public async Task GetDogBreed()
        {
            var currentDogBreeds = await GetValues();

            if (currentDogBreeds != null)
            {
                dogBreeds.Clear();
                foreach (var b in currentDogBreeds)
                    dogBreeds.Add(b);
            }
        }
    }
}
