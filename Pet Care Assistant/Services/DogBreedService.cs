using Pet_Care_Assistant.Models;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;

namespace Pet_Care_Assistant.Services
{
    public class DogBreedService
    {
        private const string Url = "https://gist.githubusercontent.com/kastriotadili/acc722c9858189440d964db976303078/raw/ba63ffd45a76e54fd816ff471e9f3ac348e983b7/dog-breeds-data.json";

        public List<DogBreed> getDogBreed()
        {
            using var client = new System.Net.Http.HttpClient();

            // JSON root is { "dogBreeds": [ ... ] } — deserialize into wrapper
            var wrapper = client.GetFromJsonAsync<DogBreedsResponse>(Url)
                .GetAwaiter().GetResult();

            var breeds = wrapper?.DogBreeds ?? new List<DogBreed>();
            System.Diagnostics.Debug.WriteLine("Dog breeds fetched: " + breeds.Count);
            return breeds;
        }

        private class DogBreedsResponse
        {
            [System.Text.Json.Serialization.JsonPropertyName("dogBreeds")]
            public List<DogBreed> DogBreeds { get; set; } = new();
        }
    }
}
