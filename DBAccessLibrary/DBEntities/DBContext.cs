using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace DBAccessLibrary.DBEntities
{
    /// <summary>
    ///  This is the Entity Framework data context of the project.
    /// </summary>
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Manifacturer> Manifacturers { get; set; }
        public virtual DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleView> VehicleViewRecords { get; set; }
        public DbSet<CategoryView> CategoryViewRecords { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Views are added to model.
            modelBuilder.Entity<VehicleView>(entity => {
                entity.HasKey(e => e.Id);
                entity.ToTable("VehicleView");
            });

            modelBuilder.Entity<CategoryView>(entity => {
                entity.HasKey(e => e.Id);
                entity.ToTable("CategoryView");
            });
        }
    }
}
