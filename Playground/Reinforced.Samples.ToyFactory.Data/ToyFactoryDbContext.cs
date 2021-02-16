using Microsoft.EntityFrameworkCore;
using Reinforced.Samples.ToyFactory.Logic.Entities;

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
            OnWarehouseModelCreating(modelBuilder);
        }
    }
}