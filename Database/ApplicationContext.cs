using Lab_2.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab__2_.Database
{
    public class ApplicationContext:DbContext 
    {
        public DbSet<AdministratorVm> Administrators { get; set; }
        public DbSet<ClientVm> Clients { get; set; }
        public DbSet<CarsVm> Cars { get; set; }
        public DbSet<RentalCarVm> RentalCars { get; set;}
        public DbSet<RentalFormVm> RentalForm { get; set; }
        public DbSet<OrderVm> Order { get; set; }
        public ApplicationContext()
        {
            Database.Migrate();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=localhost;Database=Lab_4;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }
}
