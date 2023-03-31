using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Setup.Models
{
    public class Person
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }


        

        public int Id { get; set; }
        [Required]
        public string Email { get; set; }

        [InverseProperty("User1")]
        public List<Friendship> Friends { get; set; }


        public Person(){}
        public Person(string name, int id, List<Friendship> friends)
        {
            Username = name;
            Id = id;
            Friends = friends;
        }
    }
}
