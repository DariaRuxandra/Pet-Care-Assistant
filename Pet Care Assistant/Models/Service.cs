// În folderul Models/Service.cs
namespace Pet_Care_Assistant.Models
{
    public class Service
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string ImageUrl { get; set; }
        public string TreatmentPlan { get; set; } // Pentru "scheme de tratament"
    }
}