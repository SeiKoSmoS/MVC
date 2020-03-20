using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace WebAppEnterprise
{
    public partial class EnterpriseContext : DbContext
    {
        public EnterpriseContext()
        {
        }

        public EnterpriseContext(DbContextOptions<EnterpriseContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Budget> Budget { get; set; }
        public virtual DbSet<FinishedProducts> FinishedProducts { get; set; }
        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<IngredientsView> IngredientsView { get; set; }
        public virtual DbSet<Payroll> Payroll { get; set; }
        public virtual DbSet<Positions> Positions { get; set; }
        public virtual DbSet<ProductSales> ProductSales { get; set; }
        public virtual DbSet<Production> Production { get; set; }
        public virtual DbSet<PurchaseOfMaterials> PurchaseOfMaterials { get; set; }
        public virtual DbSet<RawMaterials> RawMaterials { get; set; }
        public virtual DbSet<Stuff> Stuff { get; set; }
        public virtual DbSet<Units> Units { get; set; }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Budget>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.AmountOfBudget).HasColumnType("money");
            });

            modelBuilder.Entity<FinishedProducts>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.UnitNavigation)
                    .WithMany(p => p.FinishedProducts)
                    .HasForeignKey(d => d.Unit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_FinishedProducts_Units");
            });

            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.HasOne(d => d.MaterialNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.Material)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredients_RawMaterials");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.Ingredients)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Ingredients_FinishedProducts");
            });

            modelBuilder.Entity<IngredientsView>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("IngredientsView");

                entity.Property(e => e.Material)
                    .IsRequired()
                    .HasColumnName("material")
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.Property(e => e.Product)
                    .IsRequired()
                    .HasColumnName("product")
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<Payroll>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.FinalSalary).HasComputedColumnSql("([Bonus]+[Salary])");

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithMany(p => p.Payroll)
                    .HasForeignKey(d => d.Employee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Payroll_Stuff");
            });

            modelBuilder.Entity<Positions>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();
            });

            modelBuilder.Entity<ProductSales>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.Employee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSales_Stuff");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.ProductSales)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ProductSales_FinishedProducts");
            });

            modelBuilder.Entity<Production>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.EmployeeNavigation)
                    .WithMany(p => p.Production)
                    .HasForeignKey(d => d.Employee)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Production_Stuff");

                entity.HasOne(d => d.ProductNavigation)
                    .WithMany(p => p.Production)
                    .HasForeignKey(d => d.Product)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Production_FinishedProducts");
            });

            modelBuilder.Entity<PurchaseOfMaterials>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Date).HasColumnType("date");

                entity.HasOne(d => d.MaterialNavigation)
                    .WithMany(p => p.PurchaseOfMaterials)
                    .HasForeignKey(d => d.Material)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_PurchaseOfMaterials_RawMaterials");
            });

            modelBuilder.Entity<RawMaterials>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Amount).HasColumnType("money");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsFixedLength();

                entity.HasOne(d => d.UnitNavigation)
                    .WithMany(p => p.RawMaterials)
                    .HasForeignKey(d => d.Unit)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_RawMaterials_Units");
            });

            modelBuilder.Entity<Stuff>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(70)
                    .IsFixedLength();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsFixedLength();

                entity.Property(e => e.PhoneNumber)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Salary).HasColumnType("money");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Stuff)
                    .HasForeignKey(d => d.Position)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Stuff_Positions");
            });

            modelBuilder.Entity<Units>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
