using Microsoft.EntityFrameworkCore;
using TechicalTask.API.Classes;
using TechicalTask.API.Data.Configuration;

namespace TechicalTask.API.Data
{
    public class ClientsDBContext : DbContext
    { 
        
        public ClientsDBContext(DbContextOptions<ClientsDBContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<Result> Results { get; set; }
        public DbSet<Experiment> Experiments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfiguration(new ExperimentConfigur());
        }
    }
}
