using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace API_BoilerPlate.DAL.Entities
{
    public partial class TestContext : DbContext
    {
        public TestContext()
        {
        }

        public TestContext(DbContextOptions<TestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<OrderShoes> OrderShoes { get; set; }
        public virtual DbSet<Shoes> Shoes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Orders>(entity =>
            {
                entity.Property(e => e.Date)
                    .HasColumnType("datetime");

                entity.Property(e => e.OrderedBy)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<OrderShoes>(entity =>
            {
                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderShoes)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderShoes_Orders");

                entity.HasOne(d => d.Shoe)
                    .WithMany(p => p.OrderShoes)
                    .HasForeignKey(d => d.ShoeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OrderShoes_Shoes");
            });

            modelBuilder.Entity<Shoes>(entity =>
            {
                entity.Property(e => e.Added).HasColumnType("datetime");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("money");
            });
        }
    }
}
