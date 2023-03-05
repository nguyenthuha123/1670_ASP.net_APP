using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using _1670_Final.Models;
using Microsoft.AspNetCore.Identity;

namespace _1670_Final.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<_1670_Final.Models.Book> Book { get; set; }
        public DbSet<_1670_Final.Models.Order> Order { get; set; }
        public DbSet<_1670_Final.Models.User> User { get; set; }
    }
}