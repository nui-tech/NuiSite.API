using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace NuiSite.API.Entities
{
    public partial class NuiSiteContext : DbContext
    {
        public virtual DbSet<Post> Post { get; set; }

        public NuiSiteContext(DbContextOptions<NuiSiteContext> options) : base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Post>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(250);
            });
        }
    }
}