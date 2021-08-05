using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace Database
{
    public partial class RateDatabaseContext : DbContext
    {
        public RateDatabaseContext()
            : base("name=RateDatabaseContext")
        {
        }

        public virtual DbSet<Checking> Checkings { get; set; }
        public virtual DbSet<Suscriber> Suscribers { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Checking>()
                .Property(e => e.YenValue)
                .HasPrecision(19, 4);
        }
    }
}
