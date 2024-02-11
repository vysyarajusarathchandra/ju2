using ju.entity;
using Microsoft.EntityFrameworkCore;

namespace ju.Database
{
    public class Mycontext:DbContext
    {
        public DbSet<User>Users { get; set; }
        public DbSet<Book> Books { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server=MAv-SQLSERVER;Database=sarath12345678912;Trusted_Connection=True;MultipleActiveREsultSets=True");
            optionsBuilder.UseSqlServer("Data source = SARATH; Initial Catalog=Sarath1456789;User Id=akki;Password=1234;TrustServerCertificate=true");
        }
    }
}
