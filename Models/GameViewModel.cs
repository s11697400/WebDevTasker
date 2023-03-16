using Microsoft.AspNetCore.Mvc;

namespace Setup.Models
{
    public class GameViewModel 
    {
        public Game game;
        public GameViewModel(Game game)
        {
            this.game = game;
         
        }
    
    }

}
