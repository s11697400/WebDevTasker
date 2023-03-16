using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;

namespace Setup.Models
{
    public class Game
    {
        public int HighScore = 0;
        public string ImageNameCharachter = "Person.png";
        public string Background = "background.jpg";
        public string ObjectImg = "Comet.png";
        
    }
}
