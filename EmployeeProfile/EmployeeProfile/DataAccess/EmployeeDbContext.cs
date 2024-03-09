
using EmployeeProfile.Models;
using EmployeeProfile.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeProfile.DataAccess
{
    public class EmployeeDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set;}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string conexionDB = $"Filename={ConnectionDB.ReturnPath("employees.db")}";
            optionsBuilder.UseSqlite(conexionDB);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(col => col.IdEmployee);
                entity.Property(col => col.IdEmployee).IsRequired().ValueGeneratedOnAdd();
            });
        }

    }
}
