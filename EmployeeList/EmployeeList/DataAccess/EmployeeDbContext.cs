
using EmployeeList.Models;
using EmployeeList.Utilities;
using Microsoft.EntityFrameworkCore;

namespace EmployeeList.DataAccess
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
