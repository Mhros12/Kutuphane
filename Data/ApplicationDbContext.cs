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
        public DbSet<KayitModel> Kayitlar { get; set; }
        public DbSet<KitaplikModel> Kitapliklar { get; set; }

    }
}

