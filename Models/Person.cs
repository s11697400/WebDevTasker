using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

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
        
        public Person(){}
        public Person(string name, int id)
        {
            Username = name;
            Id = id;
        }
    }
}
