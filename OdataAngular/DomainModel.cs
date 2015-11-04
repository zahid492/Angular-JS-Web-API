namespace OdataAngular
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class DomainModel : DbContext
    {
        public DomainModel()
            : base("name=DomainModel1")

        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public virtual DbSet<Basic_Information> Basic_Information { get; set; }
        public virtual DbSet<Class> Classes { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Class>()
                .HasMany(e => e.Basic_Information)
                .WithRequired(e => e.Class)
                .HasForeignKey(e => e.Class_id)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.Basic_Information)
                .WithRequired(e => e.Department)
                .HasForeignKey(e => e.Department_id)
                .WillCascadeOnDelete(false);
        }
    }
}
