using KicWeb.Models;
using Microsoft.EntityFrameworkCore;

namespace KicWeb.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }
        public DbSet<ServiceCategory> ServiceCategories { get; set; }
    }
}
