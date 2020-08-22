using Microsoft.EntityFrameworkCore;
using Reinforced.Samples.ToyFactory.Logic.Entities;

namespace Reinforced.Samples.ToyFactory.Data
{
    public class ToyFactoryDbContext : DbContext
    {
        private const string DefaultConnection =
            @"Data Source=.\MSSQL2017;Initial Catalog=ToyFactory;Integrated Security=True";

        private readonly string _connectionString;
        public ToyFactoryDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ToyFactoryDbContext() : this(DefaultConnection) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlueprintResources>().HasKey(x => new { x.BlueprintId, x.ResourceId });
        }
        public DbSet<ToyType> ToyTypes { get; set; }
        public DbSet<Blueprint> Blueprints { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<BlueprintResources> BlueprintResources { get; set; }
    }
}
