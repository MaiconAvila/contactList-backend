using ContactList.Models;
using Microsoft.EntityFrameworkCore;

namespace ContactList.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Contacts> Contacts { get; set; }
        public DbSet<Phone> Phone { get; set; }
        public DbSet<Email> Email { get; set; }
        public DbSet<Whatsapp> Whatsapp { get; set; }

        protected override void OnConfiguring(
            DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(connectionString: "DataSource=app.db;Cache=Shared");
        }
    }
}
