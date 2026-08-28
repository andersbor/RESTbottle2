using Microsoft.AspNetCore.Identity;

namespace RESTbottle2.Models
{
    public class Bottle
    {
        public int Id { get; set; }
        public double Volume { get; set; }
        public string? Name { get; set; } 

        

        public override string ToString()
        {
            return $"Bottle: {Name}, Volume: {Volume}";
        }
    }
}
