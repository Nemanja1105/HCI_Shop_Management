using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MySql.EntityFrameworkCore.Extensions;

namespace HCI_Projekat_1.Models;

public partial class ShopManagementContext : DbContext
{
    public ShopManagementContext()
    {
    }

    public ShopManagementContext(DbContextOptions<ShopManagementContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Bill> Bill { get; set; }

    public virtual DbSet<Billitem> Billitem { get; set; }

    public virtual DbSet<Canceledbill> Canceledbill { get; set; }

    public virtual DbSet<Category> Category { get; set; }

    public virtual DbSet<Employee> Employee { get; set; }

    public virtual DbSet<Procurement> Procurement { get; set; }

    public virtual DbSet<Procurementitem> Procurementitem { get; set; }

    public virtual DbSet<Product> Product { get; set; }

    public virtual DbSet<Supplier> Supplier { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=shop_management;uid=root;pwd=Nemanja@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Bill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("bill");

            entity.HasIndex(e => e.EmployeeId, "fk_Racun_Radnik1_idx");

            entity.Property(e => e.DateOfIssue).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            entity.Property(e => e.PaymentType).HasColumnType("bit(1)");
            entity.Property(e => e.TotalPrice).HasPrecision(7, 2);

            entity.HasOne(d => d.Employee).WithMany(p => p.Bill)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Racun_Radnik1");
        });

        modelBuilder.Entity<Billitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("billitem");

            entity.HasIndex(e => e.ProductId, "fk_Stavka_Proizvod1_idx");

            entity.HasIndex(e => e.BillId, "fk_Stavka_Racun1_idx");

            entity.Property(e => e.BillId).HasColumnName("Bill_id");
            entity.Property(e => e.Price).HasPrecision(7, 2);
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.Quantity).HasPrecision(7, 2);

            entity.HasOne(d => d.Bill).WithMany(p => p.Billitem)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Stavka_Racun1");

            entity.HasOne(d => d.Product).WithMany(p => p.Billitem)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Stavka_Proizvod1");
        });

        modelBuilder.Entity<Canceledbill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("canceledbill");

            entity.HasIndex(e => e.BillId, "fk_STORNIRANI_RACUNI_Racun1_idx");

            entity.HasIndex(e => e.EmployeeId, "fk_STORNIRANI_RACUNI_Radnik1_idx");

            entity.Property(e => e.BillId).HasColumnName("Bill_Id");
            entity.Property(e => e.Date).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            entity.Property(e => e.Reason)
                .IsRequired()
                .HasMaxLength(45);

            entity.HasOne(d => d.Bill).WithMany(p => p.Canceledbill)
                .HasForeignKey(d => d.BillId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_STORNIRANI_RACUNI_Racun1");

            entity.HasOne(d => d.Employee).WithMany(p => p.Canceledbill)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_STORNIRANI_RACUNI_Radnik1");
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("category");

            entity.Property(e => e.Name).HasMaxLength(45);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("employee");

            entity.Property(e => e.Adresa)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Jmb)
                .IsRequired()
                .HasMaxLength(13)
                .IsFixedLength()
                .HasColumnName("JMB");
            entity.Property(e => e.Language).HasMaxLength(45);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Password)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Surname)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Theme)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Username)
                .IsRequired()
                .HasMaxLength(45);
        });

        modelBuilder.Entity<Procurement>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("procurement");

            entity.HasIndex(e => e.SupplierId, "fk_Nabavka_Dobavljac1_idx");

            entity.HasIndex(e => e.EmployeeId, "fk_Nabavka_Radnik1_idx");

            entity.Property(e => e.DateOfAcquisition).HasColumnType("datetime");
            entity.Property(e => e.EmployeeId).HasColumnName("Employee_Id");
            entity.Property(e => e.SupplierId).HasColumnName("Supplier_Id");

            entity.HasOne(d => d.Employee).WithMany(p => p.Procurement)
                .HasForeignKey(d => d.EmployeeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Nabavka_Radnik1");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Procurement)
                .HasForeignKey(d => d.SupplierId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Nabavka_Dobavljac1");
        });

        modelBuilder.Entity<Procurementitem>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("procurementitem");

            entity.HasIndex(e => e.ProcurementId, "fk_StavkaNabavke_Nabavka1_idx");

            entity.HasIndex(e => e.ProductId, "fk_StavkaNabavke_Proizvod1_idx");

            entity.Property(e => e.Price).HasPrecision(7, 2);
            entity.Property(e => e.ProcurementId).HasColumnName("Procurement_Id");
            entity.Property(e => e.ProductId).HasColumnName("Product_Id");
            entity.Property(e => e.Quantity).HasPrecision(7, 2);

            entity.HasOne(d => d.Procurement).WithMany(p => p.Procurementitem)
                .HasForeignKey(d => d.ProcurementId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_StavkaNabavke_Nabavka1");

            entity.HasOne(d => d.Product).WithMany(p => p.Procurementitem)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_StavkaNabavke_Proizvod1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("product");

            entity.HasIndex(e => e.CategoryId, "fk_Proizvod_Vrsta_idx");

            entity.Property(e => e.Barkod)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.CategoryId).HasColumnName("Category_Id");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(100);
            entity.Property(e => e.PurchasePrice).HasPrecision(7, 2);
            entity.Property(e => e.Quantity).HasPrecision(7, 2);
            entity.Property(e => e.SellingPrice).HasPrecision(7, 2);
            entity.Property(e => e.UnitOfMeasure)
                .IsRequired()
                .HasMaxLength(45);

            entity.HasOne(d => d.Category).WithMany(p => p.Product)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Proizvod_Vrsta");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("supplier");

            entity.Property(e => e.Address)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(45);
            entity.Property(e => e.PhoneNumber)
                .IsRequired()
                .HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
