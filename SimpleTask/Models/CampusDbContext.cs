using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SimpleTask.Models
{
    public partial class CampusDbContext : DbContext
    {
        public virtual DbSet<Accounts> Accounts { get; set; }
        public virtual DbSet<Classrooms> Classrooms { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }

        public CampusDbContext(DbContextOptions<CampusDbContext> options)
            : base (options)
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
               optionsBuilder.UseSqlServer(@"Server=c3dev.clxfrcyfd6hl.us-east-1.rds.amazonaws.com;Database=c3;User Id=dev1;Password=Password123;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Accounts>(entity =>
            {
                entity.Property(e => e.Guid).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Status).HasDefaultValueSql("('A')");

                entity.Property(e => e.SubDomain).IsUnicode(false);
            });

            modelBuilder.Entity<Classrooms>(entity =>
            {
                entity.Property(e => e.Capacity).HasDefaultValueSql("((99))");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Classrooms)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_Classrooms_Locations");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.Property(e => e.IsActive).HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Account)
                    .WithMany(p => p.Locations)
                    .HasForeignKey(d => d.AccountId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Locations_Account");
            });
        }
    }
}
