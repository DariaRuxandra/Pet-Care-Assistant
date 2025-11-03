using SQLite;
using System;
using System.ComponentModel.DataAnnotations;

namespace Pet_Care_Assistant.Models
{
    public class Pet
    {

       
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public string Breed { get; set; } = "";
        public int Age { get; set; }
        public DateTime dateOfBirth { get; set; } = DateTime.Now;
        public string OwnerName { get; set; } = "";
        public string OwnerContact { get; set; } = "";

        public Pet(int id, string name, string species, string breed, int age, DateTime dateOfBirth, string ownerName, string ownerContact)
        {
            Id = id;
            Name = name;
            Species = species;
            Breed = breed;
            Age = age;
            this.dateOfBirth = dateOfBirth;
            OwnerName = ownerName;
            OwnerContact = ownerContact;
        }
    }
}