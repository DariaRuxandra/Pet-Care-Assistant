using System;
using System.ComponentModel.DataAnnotations;

namespace Pet_Care_Assistant.Models
{
    public class Pet
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public string Breed { get; set; } = "";
        public int Age { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}