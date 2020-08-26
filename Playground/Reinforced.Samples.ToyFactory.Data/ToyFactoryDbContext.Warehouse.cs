using Microsoft.EntityFrameworkCore;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;

namespace Reinforced.Samples.ToyFactory.Data
{
    public partial class ToyFactoryDbContext
    {
        public DbSet<MeasurementUnit> MeasurementUnits { get; set; }
        public DbSet<ResourceSupply> ResourceSupplies { get; set; }
        public DbSet<ResourceSupplyItem> ResourceSupplyItems { get; set; }
        public DbSet<ResourceSupplyStatusHistoryItem> ResourceSupplyStatusHistoryItems { get; set; }
        public DbSet<Resource> Resources { get; set; }
        protected void OnWarehouseModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ResourceSupplyItem>().HasKey(x => new { x.ResourceSupplyId, x.ResourceId });
        }
    }
}
