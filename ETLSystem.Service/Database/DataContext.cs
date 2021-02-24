using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ETLSystem.Service.Database
{
    public partial class DataContext : DbContext
    {
        public DataContext()
        {
        }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public virtual DbSet<DataS1> DataS1 { get; set; }
        public virtual DbSet<DataS2> DataS2 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<DataS1>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("DataS1_pkey");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            modelBuilder.Entity<DataS2>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .HasName("DataS2_pkey");

                entity.Property(e => e.Id).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
