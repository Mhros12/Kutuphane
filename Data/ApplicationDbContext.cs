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
        public DbSet<RegistrationModel> Registrations { get; set; }
        public DbSet<BookModel> Books { get; set; }
    }
}

