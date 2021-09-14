using Microsoft.EntityFrameworkCore;
using WebApiProject.Models;

namespace WebApiProject.Context
{
    public class ProjectContext : DbContext
    {
        public ProjectContext(DbContextOptions options) : base(options)
        {
        }
        
        public DbSet<User> Users { get; set; }
    }
}