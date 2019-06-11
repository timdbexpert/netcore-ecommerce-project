using Services.Models;
using System.Data.Entity;

namespace Services.Context
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext() : base("ConnDB") { }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
        }

        public DbSet<UserEntity> User { get; set; }
        public DbSet<ProductEntity> Product { get; set; }
    }
}
