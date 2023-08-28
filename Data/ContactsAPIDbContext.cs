using ContactsAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactsAPI.Data
{
    public class ContactsAPIDbContext : DbContext
    {

        //create a contructor.. . . 

        public ContactsAPIDbContext(DbContextOptions options) : base (options) 
        {
                
        }

        //create properties add tables 
        public DbSet<Contact> Contacts {get ; set;}
        public DbSet<Product> Products { get; set; }
    }
}
