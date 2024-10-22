using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace Lab2.DataAccess
{
    public class OwnerDbContext : DbContext
    {
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Animal> Animals { get; set; }
        public OwnerDbContext()
        {

        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\User\\source\\repos\\Lab2.DataAccess\\Lab2.DataAccess\\OwnerDb.mdf;Integrated Security=True");
        }
    }
}