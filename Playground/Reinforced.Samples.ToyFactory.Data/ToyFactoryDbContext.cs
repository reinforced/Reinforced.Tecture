using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;

namespace Reinforced.Samples.ToyFactory.Data
{
    public partial class ToyFactoryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var location = System.Reflection.Assembly.GetExecutingAssembly().Location;
            var stringToSearch = "Reinforced.Samples.ToyFactory";
            var pos = location.IndexOf(stringToSearch);
            var removed = location.Remove(pos + stringToSearch.Length, location.Length - pos - stringToSearch.Length);
            var resultPath = removed + ".Data\\TectureSample.db";
            
            optionsBuilder.UseSqlite($"Data Source={resultPath};Cache=Shared");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlueprintResources>().HasKey(x => new {x.BlueprintId, x.ResourceId});
            modelBuilder.Entity<ResourceSupplyItem>().HasKey(x => new {x.ResourceSupplyId, x.ResourceId});
        }
    }
}