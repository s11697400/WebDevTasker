using Microsoft.AspNetCore.Mvc;
using Setup.Areas.Identity.Data;
using System.Security.Cryptography.X509Certificates;

namespace Setup.Models
{
    public class Game
    {
        
 /*       public string ImageNameCharachter = "Person.png";
        public string Background = "background.jpg";
        public string ObjectImg = "Comet.png";*/
        public string PlayerId { get; set; }
        public int score { get; set; }
        public Game()
        {

        }
        public Game(string? playerId, int score)
        {
            PlayerId = playerId;
            this.score = score;
        }
    }
}
