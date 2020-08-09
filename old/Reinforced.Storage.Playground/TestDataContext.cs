using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Reinforced.Storage.Playground.Entities;

namespace Reinforced.Storage.Playground
{
    public class TestDataContext : DbContext
    {
        public DbSet<Item> Items { get; set; }

        public DbSet<Order> Orders { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Document> Documents { get; set; }

        public DbSet<OrderDoc> OrderDocs { get; set; }

        /// <summary>
        /// This method is called when the model for a derived context has been initialized, but
        /// before the model has been locked down and used to initialize the context.  The default
        /// implementation of this method does nothing, but it can be overridden in a derived class
        /// such that the model can be further configured before it is locked down.
        /// </summary>
        /// <remarks>
        /// Typically, this method is called only once when the first instance of a derived context
        /// is created.  The model for that context is then cached and is for all further instances of
        /// the context in the app domain.  This caching can be disabled by setting the ModelCaching
        /// property on the given ModelBuidler, but note that this can seriously degrade performance.
        /// More control over caching is provided through use of the DbModelBuilder and DbContextFactory
        /// classes directly.
        /// </remarks>
        /// <param name="modelBuilder"> The builder that defines the model for the context being created. </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<LanguageUser>().HasKey(d => new {d.LanguageCode, d.VendorId});
            modelBuilder.Entity<Item>()
                .HasOptional(d => d.LanguageUser)
                .WithMany()
                .HasForeignKey(d => new {d.LanguageCode, d.VendorId});

            modelBuilder.Entity<OrderDoc>().ToTable("OrderDocs");
        }
    }
}
