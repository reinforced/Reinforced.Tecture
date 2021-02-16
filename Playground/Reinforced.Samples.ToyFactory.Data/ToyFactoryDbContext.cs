using Microsoft.EntityFrameworkCore;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;

namespace Reinforced.Samples.ToyFactory.Data
{
    public partial class ToyFactoryDbContext : DbContext
    {
        private const string DefaultConnection = "Data Source=TectureSample.db;Cache=Shared";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite(DefaultConnection);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlueprintResources>().HasKey(x => new {x.BlueprintId, x.ResourceId});
            modelBuilder.Entity<ResourceSupplyItem>().HasKey(x => new {x.ResourceSupplyId, x.ResourceId});
        }
    }
}