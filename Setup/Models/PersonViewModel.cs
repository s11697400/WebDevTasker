using Microsoft.AspNetCore.Mvc;

namespace Setup.Models
{
    public class PersonViewModel
    {
        public Person _Person;

        public PersonViewModel(Person p) {
            _Person = p;
        }
        

        public string Name => _Person.Username;
        public int Id => _Person.Id;
    }
}
