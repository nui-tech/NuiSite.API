using Microsoft.EntityFrameworkCore;

namespace NuiSite.API.Entities
{
    public partial class NuiSiteContext : DbContext
    {
        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<User> User { get; set; }

        public NuiSiteContext(DbContextOptions<NuiSiteContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).HasMaxLength(50);
            });

            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UpdatedOn).HasColumnType("date");

                entity.HasOne(d => d.CreatedByNavigation)
                    .WithMany(p => p.Post)
                    .HasForeignKey(d => d.CreatedBy)
                    .HasConstraintName("FK_Post_User");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.AvatarUrl).HasColumnType("varchar(max)");

                entity.Property(e => e.Email).HasColumnType("varchar(100)");

                entity.Property(e => e.FirebaseId).HasColumnType("varchar(50)");

                entity.Property(e => e.Name).HasMaxLength(100);
            });
        }
    }
}