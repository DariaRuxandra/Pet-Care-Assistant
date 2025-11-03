using SQLite;
using System;

namespace Pet_Care_Assistant.Models
{
    public class PetDto
    {
        [PrimaryKey]
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public string Species { get; set; } = "";
        public string Breed { get; set; } = "";
        public int Age { get; set; }
        public DateTime dateOfBirth { get; set; } = DateTime.Now;
        public string OwnerName { get; set; } = "";
        public string OwnerContact { get; set; } = "";
    }
}
