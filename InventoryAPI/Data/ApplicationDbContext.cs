using InventoryAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryAPI.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
