using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using ST10117268_PROG7311_TASK2.Temp;

#nullable disable

namespace ST10117268_PROG7311_TASK2.Models
{
    public partial class st10117268Context : DbContext
    {
        public st10117268Context()
        {
        }

        public st10117268Context(DbContextOptions<st10117268Context> options)
            : base(options)
        {
        }

        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<UsersProduct> UsersProducts { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
 //               optionsBuilder.UseSqlServer("Server=KIAAN;Database=st10117268;Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.ProductName)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("fullname");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Role)
                    .IsRequired()
                    .HasMaxLength(25)
                    .IsUnicode(false)
                    .HasColumnName("role");
            });

            modelBuilder.Entity<UsersProduct>(entity =>
            {
                entity.ToTable("Users_Product");

                entity.Property(e => e.UsersProductId).HasColumnName("usersProductID");

                entity.Property(e => e.ProductDate)
                    .HasColumnType("date")
                    .HasColumnName("product_date");

                entity.Property(e => e.ProductId).HasColumnName("productID");

                entity.Property(e => e.ProductType)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("product_type");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.UserId).HasColumnName("userID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.UsersProducts)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users_Pro__produ__2A4B4B5E");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UsersProducts)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Users_Pro__userI__29572725");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

        public DbSet<ST10117268_PROG7311_TASK2.Temp.TempLogin> TempLogin { get; set; }

        public DbSet<ST10117268_PROG7311_TASK2.Temp.TempRegister> TempRegister { get; set; }

        public DbSet<ST10117268_PROG7311_TASK2.Temp.TempUserProducts> TempUserProducts { get; set; }

        public DbSet<ST10117268_PROG7311_TASK2.Temp.TempPickFarmer> TempPickFarmer { get; set; }

        public DbSet<ST10117268_PROG7311_TASK2.Temp.TempFilterDate> TempFilterDate { get; set; }

        public DbSet<ST10117268_PROG7311_TASK2.Temp.TempFilterProductType> TempFilterProductType { get; set; }
    }
}
