using Microsoft.AspNetCore.Mvc;

namespace Setup.Models
{
    public class Skills
    {
        public string name { get; set; }
        public int rating { get; set; }
        public Skills(string name, int rating)
        {
            this.name = name;
            this.rating = rating;
        }
    }
}
