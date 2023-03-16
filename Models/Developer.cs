using Microsoft.AspNetCore.Mvc;

namespace Setup.Models
{
    public class Developer
    {
        public int id = 1;
        public string Name = "Thijs Soepenberg";
        public string Description = "Mijn naam is Thijs Soepenberg. Ik zit op het Windesheim in Zwolle. Ook woon ik in Zwolle. \r\n                Mijn hobby's zijn sportschool, voetbal, poker en gitaar spelen.\r\n                Als er meer vragen zijn kun je mij een berichtje doen via het contact formulier.";
        public string ImageName = "Thijs.jpg";
        public List<Skills> Skills = new List<Skills>(new[]
        {
            new Skills("PHP", 10),
            new Skills("JS", 10)
        });

    }
}
