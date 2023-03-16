using Microsoft.AspNetCore.Mvc;

namespace Setup.Models
{
    public class ProfileViewModel
    {
        Developer _developer;
        public ProfileViewModel(Developer developer)
        {
            _developer = developer;
        }
        public string name => _developer.Name;
        public string description => _developer.Description;
        public int id => _developer.id;
        public string ImageURL => _developer.ImageName;
        public List<Skills> skills => _developer.Skills;


    }
}
