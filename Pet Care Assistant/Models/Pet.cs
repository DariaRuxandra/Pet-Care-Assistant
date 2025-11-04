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
        public DateTime dateOfBirth { get; set; } = DateTime.Now;
        public string OwnerName { get; set; } = "";
        public string OwnerContact { get; set; } = "";

        public Pet(int id, string name, string species, string breed, DateTime dateOfBirth, string ownerName, string ownerContact)
        {
            Id = id;
            Name = name;
            Species = species;
            Breed = breed;
            this.dateOfBirth = dateOfBirth;
            OwnerName = ownerName;
            OwnerContact = ownerContact;
        }

        //this function calculates the age of the dog based on the date of birth
        public int Age
        {
            get
            {
                var today = DateTime.Today;
                var age = today.Year - dateOfBirth.Year;
                if (dateOfBirth.Date > today.AddYears(-age))
                    age--;
                return Math.Max(0, age);
            }
        }

        public string AgeDisplay => $"{Age} year{(Age == 1 ? "" : "s")} old";
    }
}