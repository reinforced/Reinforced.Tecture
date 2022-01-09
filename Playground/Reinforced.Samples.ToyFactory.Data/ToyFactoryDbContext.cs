using Microsoft.EntityFrameworkCore;
using Reinforced.Samples.ToyFactory.Logic.Entities;
using Reinforced.Samples.ToyFactory.Logic.Warehouse.Entities.Suppliement;

namespace Reinforced.Samples.ToyFactory.Data
{
    public partial class ToyFactoryDbContext : DbContext
    {
        private const string DefaultConnection =
            @"server=127.0.0.1;port=4444;password=1qaz2wsx;user id=root;database=test;pooling=True;characterset=utf8;allowuservariables=True;convertzerodatetime=True;defaultcommandtimeout=720";

        private readonly string _connectionString;
        private ToyFactoryDbContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public ToyFactoryDbContext() : this(DefaultConnection) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySql(_connectionString,new MariaDbServerVersion("10.8"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BlueprintResources>().HasKey(x => new { x.BlueprintId, x.ResourceId });
            OnWarehouseModelCreating(modelBuilder);
        }
        public DbSet<ToyType> ToyTypes { get; set; }
        public DbSet<Blueprint> Blueprints { get; set; }
        public DbSet<BlueprintResources> BlueprintResources { get; set; }

        
    }
}
