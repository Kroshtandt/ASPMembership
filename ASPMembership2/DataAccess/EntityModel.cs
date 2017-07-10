namespace ASPMembership2.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class EntityModel : DbContext
    {
        public EntityModel()
            : base("name=EntityModelConnection")
        {
        }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<BlogItem> BlogItem { get; set; }
        public virtual DbSet<Methods> Methods { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.Methods)
                .WithMany(e => e.AspNetRoles)
                .Map(m => m.ToTable("RoleMethods").MapLeftKey("RoleId").MapRightKey("MethodId"));

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.BlogItem)
                .WithRequired(e => e.AspNetUsers)
                .HasForeignKey(e => e.CreatedBy)
                .WillCascadeOnDelete(false);
        }
    }
}
