using Microsoft.EntityFrameworkCore;
using Setup.Models;

namespace Setup.DataContext
{
    public class PersonDatabaseContext : DbContext
    {
        public PersonDatabaseContext()
        { }
        public PersonDatabaseContext(DbContextOptions<PersonDatabaseContext> options)
            : base(options)
        { }
        public virtual DbSet<Person> Persons { get; set; }
    }
}
