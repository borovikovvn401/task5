using System;
using System.Collections.Generic;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace task5.Model
{
    public partial class EfModel : DbContext
    {



        public static EfModel instance;

        public static EfModel init()
        {
            if (instance == null)
                instance = new EfModel();
            return instance;
        }

        public static bool Save()
        {
            try
            {   
                EfModel.init().SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public EfModel()
        {
        }

        public EfModel(DbContextOptions<EfModel> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Dish> Dishes { get; set; } = null!;
        public virtual DbSet<OderDish> OderDishes { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;
        public virtual DbSet<Status> Statuses { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseMySql("server=cfif31.ru;database=ISPr23-47_BorovikovVN_task5;user id=ISPr23-47_BorovikovVN;password=ISPr23-47_BorovikovVN", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.36-mysql"));
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.UseCollation("utf8mb4_0900_ai_ci")
                .HasCharSet("utf8mb4");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PRIMARY");

                entity.ToTable("Category");

                entity.Property(e => e.IdCategory).HasColumnName("idCategory");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Dish>(entity =>
            {
                entity.HasKey(e => e.Iddish)
                    .HasName("PRIMARY");

                entity.ToTable("dish");

                entity.HasIndex(e => e.Category, "fx_category_key_idx");

                entity.Property(e => e.Iddish).HasColumnName("iddish");

                entity.Property(e => e.Compound).HasMaxLength(120);

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");

                entity.Property(e => e.Price).HasPrecision(5,2);

                entity.HasOne(d => d.CategoryNavigation)
                    .WithMany(p => p.Dishes)
                    .HasForeignKey(d => d.Category)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_category_key");
            });

            modelBuilder.Entity<OderDish>(entity =>
            {
                entity.HasKey(e => new { e.OrderId, e.DishId })
                    .HasName("PRIMARY")
                    .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

                entity.ToTable("oder_dish");

                entity.HasIndex(e => e.DishId, "fx_dishid_key_idx");

                entity.Property(e => e.OrderId).HasColumnName("orderID");

                entity.Property(e => e.DishId).HasColumnName("dishID");

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.HasOne(d => d.Dish)
                    .WithMany(p => p.OderDishes)
                    .HasForeignKey(d => d.DishId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_dishid_key");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OderDishes)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_orderid_key");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PRIMARY");

                entity.ToTable("Order");

                entity.HasIndex(e => e.StatusStatusid, "fx_statusid_key_idx");

                entity.HasIndex(e => e.UserUserid, "fx_userid_key_idx");

                entity.Property(e => e.IdOrder).HasColumnName("idOrder");

                entity.Property(e => e.DateTime).HasColumnType("datetime");

                entity.Property(e => e.Details)
                    .HasMaxLength(45)
                    .HasColumnName("details");

                entity.Property(e => e.StatusStatusid).HasColumnName("status_statusid");

                entity.Property(e => e.Table).HasColumnName("table");

                entity.Property(e => e.UserUserid).HasColumnName("user_userid");

                entity.HasOne(d => d.StatusStatus)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.StatusStatusid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_statusid_key");

                entity.HasOne(d => d.UserUser)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserUserid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_userid_key");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.IdRole)
                    .HasName("PRIMARY");

                entity.ToTable("Role");

                entity.Property(e => e.IdRole).HasColumnName("idRole");

                entity.Property(e => e.Name).HasMaxLength(45);
            });

            modelBuilder.Entity<Status>(entity =>
            {
                entity.HasKey(e => e.Idstatus)
                    .HasName("PRIMARY");

                entity.ToTable("status");

                entity.Property(e => e.Idstatus).HasColumnName("idstatus");

                entity.Property(e => e.Name)
                    .HasMaxLength(45)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.IdUsers)
                    .HasName("PRIMARY");

                entity.HasIndex(e => e.Role, "fx_role_key_idx");

                entity.Property(e => e.IdUsers).HasColumnName("idUsers");

                entity.Property(e => e.Fio)
                    .HasMaxLength(45)
                    .HasColumnName("FIO");

                entity.Property(e => e.Login).HasMaxLength(45);

                entity.Property(e => e.Password).HasMaxLength(45);

                entity.HasOne(d => d.RoleNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.Role)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fx_role_key");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
