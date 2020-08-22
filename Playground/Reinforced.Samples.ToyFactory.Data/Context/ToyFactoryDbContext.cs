using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Reinforced.Samples.ToyFactory.Logic.Entities;

namespace Reinforced.Samples.ToyFactory.Data.Context
{
    public class ToyFactoryDbContext : DbContext
    {
        public ToyFactoryDbContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<ToyType> ToyTypes { get; set; }
        public DbSet<Blueprint> Blueprints { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<BlueprintResources> BlueprintResources { get; set; }
    }
}
