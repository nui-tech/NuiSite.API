using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NuiSite.API.Entities
{
    public partial class NuiSiteContext : DbContext
    {
        public virtual DbSet<Post> Post { get; set; }
        public virtual DbSet<Tag> Tag { get; set; }
        public virtual DbSet<TagMap> TagMap { get; set; }
        public virtual DbSet<User> User { get; set; }

        public NuiSiteContext(DbContextOptions<NuiSiteContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Author).HasMaxLength(50);

                entity.Property(e => e.CreatedOn).HasColumnType("date");

                entity.Property(e => e.Title).HasMaxLength(250);

                entity.Property(e => e.UpdateOn).HasColumnType("date");
            });

            modelBuilder.Entity<Tag>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(100);
            });

            modelBuilder.Entity<TagMap>(entity =>
            {
                entity.HasOne(d => d.Post)
                    .WithMany(p => p.TagMap)
                    .HasForeignKey(d => d.PostId)
                    .HasConstraintName("FK_TagMap_Post");

                entity.HasOne(d => d.Tag)
                    .WithMany(p => p.TagMap)
                    .HasForeignKey(d => d.TagId)
                    .HasConstraintName("FK_TagMap_Tag");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Email).HasColumnType("varchar(50)");

                entity.Property(e => e.FirebaseId).HasColumnType("nchar(30)");

                entity.Property(e => e.LastInfoUpdated).HasColumnType("date");

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.RoleId).HasColumnType("nchar(10)");
            });
        }
    }
}