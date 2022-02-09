using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace HostServiceApplication
{
    public partial class DataModel : DbContext
    {
        public DataModel()
            : base("name=DataModel")
        {
        }

        public virtual DbSet<Host> Hosts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Host>()
                .Property(e => e.Name)
                .IsUnicode(false);

            modelBuilder.Entity<Host>()
                .Property(e => e.Phone)
                .IsUnicode(false);

            modelBuilder.Entity<Host>()
                .Property(e => e.Description)
                .IsUnicode(false);

            modelBuilder.Entity<Host>()
                .Property(e => e.Website)
                .IsUnicode(false);
        }
    }
}
