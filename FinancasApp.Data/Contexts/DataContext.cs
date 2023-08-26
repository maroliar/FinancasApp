using FinancasApp.Data.Entities;
using FinancasApp.Data.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasApp.Data.Contexts
{
    public class DataContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=BDFinancasApp;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UsuarioMap());
        }

        public DbSet<Usuario> Usuario { get; set; }
}
}
