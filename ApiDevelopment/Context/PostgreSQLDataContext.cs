using ApiDevelopment.Entity;
using Microsoft.EntityFrameworkCore;

namespace ApiDevelopment.Context
{
    public class PostgreSQLDataContext:DbContext
    {
        public PostgreSQLDataContext(DbContextOptions<PostgreSQLDataContext> options): base(options) { }
        public DbSet<Student> students { get; set; }
        public DbSet<Registration> registrations { get; set; }
        public DbSet<Employe> employe { get; set; }

    }
}
