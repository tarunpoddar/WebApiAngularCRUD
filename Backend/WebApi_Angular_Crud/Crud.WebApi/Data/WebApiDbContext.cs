using Crud.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Crud.WebApi.Data
{
    public class WebApiDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        public WebApiDbContext(DbContextOptions options) : base(options) { }
    }
}
