//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Pet_Care_Assistant.Models
//{
//    public class DogBreed
//    {
//        public int Id { get; set; }
//        public string Breed { get; set; }
//        public string Temperament { get; set; }
//        public string Origin { get; set; }

//        public override string ToString()
//        {
//            return Breed;
//        }
//    }
//}

using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Pet_Care_Assistant.Models
{
    public class DogBreed
    {
        [JsonPropertyName("breed")]
        public string Breed { get; set; }

        [JsonPropertyName("origin")]
        public string Origin { get; set; }

        // temperament in the JSON is an array => map to List<string>
        [JsonPropertyName("temperament")]
        public List<string> Temperament { get; set; } = new();

        // optional: map other JSON fields if you need them
        [JsonPropertyName("breedType")]
        public string BreedType { get; set; }
        [JsonPropertyName("popularity")]
        public string Popularity { get; set; }
        [JsonPropertyName("hypoallergenic")]
        public string Hypoallergenic { get; set; }
        [JsonPropertyName("intelligence")]
        public int Intelligence { get; set; }
        [JsonPropertyName("photo")]
        public string Photo { get; set; }

        public override string ToString() => Breed;
    }
}
