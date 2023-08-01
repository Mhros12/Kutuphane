using Microsoft.EntityFrameworkCore;
using kutuphane.Models;
using Microsoft.Identity.Client;

namespace kutuphane.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Registration> Registrations { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}

