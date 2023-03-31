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
        
        protected virtual void OnModelCreate(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
    /*        modelBuilder.Entity<Friendship>().HasKey(f => f.UserId1);
            modelBuilder.Entity<Friendship>().HasKey(f => f.UserId2);
            modelBuilder.Entity<Person>().HasMany(p => p.Friends).WithOne(p => p.User1).HasForeignKey().IsRequired();
            modelBuilder.Entity<Person>().HasMany(p => p.Friends).WithOne(p => p.User2).IsRequired().HasForeignKey();*/
           
         /*   modelBuilder.Entity<Friendship>()
                .HasRequired(f => f.User2)
                .WithMany()
                .HasForeignKey(f => f.UserId2)
                .WillCascadeOnDelete(false);*/
        }
        public virtual DbSet<Person> Persons { get; set; }
      /*  public virtual DbSet<Friendship> Friends { get; set; }*/

    }
}
