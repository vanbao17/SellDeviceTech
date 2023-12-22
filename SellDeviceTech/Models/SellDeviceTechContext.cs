using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SellDeviceTech.Models
{
    public partial class SellDeviceTechContext : DbContext
    {
        public SellDeviceTechContext()
        {
        }

        public SellDeviceTechContext(DbContextOptions<SellDeviceTechContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Cart> Cart { get; set; }
        public virtual DbSet<Cates> Cates { get; set; }
        public virtual DbSet<Customers> Customers { get; set; }
        public virtual DbSet<GoogleProfile> GoogleProfile { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<Roles> Roles { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Data Source=LAPTOP-9DPP351S;Initial Catalog=SellDeviceTech;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(e => e.IdCart);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK__Cart__CustomerID__3C69FB99");

                entity.HasOne(d => d.IdNavigation)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.Id)
                    .HasConstraintName("FK__Cart__Id__3D5E1FD2");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.Cart)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__Cart__IdProduct__2B3F6F97");
            });

            modelBuilder.Entity<Cates>(entity =>
            {
                entity.HasKey(e => e.IdCate);

                entity.Property(e => e.IdCate).HasColumnName("Id_cate");

                entity.Property(e => e.Title).HasMaxLength(50);
            });

            modelBuilder.Entity<Customers>(entity =>
            {
                entity.HasKey(e => e.CustomerId);

                entity.Property(e => e.CustomerId).HasColumnName("CustomerID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.FirstName).HasMaxLength(50);

                entity.Property(e => e.LastName).HasMaxLength(50);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__Customers__RoleI__2E1BDC42");
            });

            modelBuilder.Entity<GoogleProfile>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .ValueGeneratedNever();

                entity.Property(e => e.DisplayName).HasMaxLength(50);

                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.GoogleProfile)
                    .HasForeignKey(d => d.RoleId)
                    .HasConstraintName("FK__GooglePro__RoleI__30F848ED");
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.IdProduct);

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.IdCate).HasColumnName("Id_cate");

                entity.Property(e => e.Image)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.TitleProduct)
                    .HasColumnName("Title_product")
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdCateNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCate)
                    .HasConstraintName("FK__Products__Id_cat__286302EC");
            });

            modelBuilder.Entity<Roles>(entity =>
            {
                entity.HasKey(e => e.RoleId);

                entity.Property(e => e.RoleId).HasColumnName("RoleID");

                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });
        }
    }
}
