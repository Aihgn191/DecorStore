using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DecorStore.Models;

public partial class DecorStoreDbContext : IdentityDbContext<UserCustom>
{
    

    public DecorStoreDbContext(DbContextOptions<DecorStoreDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Favorite> Favorites { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Producer> Producers { get; set; }

    public virtual DbSet<Product> Products { get; set; }

   

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder); // Gọi phương thức của lớp cơ sở để thiết lập các cấu hình mặc định của Identity

        modelBuilder.Entity<IdentityUserRole<string>>().HasKey(p => new { p.UserId, p.RoleId });
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.IdCate).HasName("PK__Category__7A169070B143E042");

            entity.ToTable("Category");

            entity.Property(e => e.IdCate).HasColumnName("ID_CATE");
            entity.Property(e => e.CateName)
                .HasMaxLength(150)
                .HasColumnName("CATE_NAME");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.Img)
                .HasMaxLength(300)
                .HasColumnName("IMG");
            entity.Property(e => e.Link)
                .HasMaxLength(300)
                .HasColumnName("LINK");
        });

        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(e => e.IdFavo).HasName("PK__Favorite__6CC98A524F17FB87");

            entity.ToTable("Favorite");

            entity.Property(e => e.IdFavo)
                .ValueGeneratedNever()
                .HasColumnName("ID_FAVO");
            entity.Property(e => e.Date)
                .HasColumnType("datetime")
                .HasColumnName("DATE");
            entity.Property(e => e.IdProd).HasColumnName("ID_PROD");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("ID_USER");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.IdOrder).HasName("PK__Order__D23A856506D6AA6F");

            entity.ToTable("Order");

            entity.Property(e => e.IdOrder).HasColumnName("ID_ORDER");
            entity.Property(e => e.Address)
                .HasColumnType("ntext")
                .HasColumnName("ADDRESS");
            entity.Property(e => e.IdUser)
                .HasMaxLength(450)
                .HasColumnName("ID_USER");
            entity.Property(e => e.IsComplete).HasColumnName("IS_COMPLETE");
            entity.Property(e => e.IsPay).HasColumnName("IS_PAY");
            entity.Property(e => e.Notes)
                .HasColumnType("ntext")
                .HasColumnName("NOTES");
            entity.Property(e => e.OrderDate)
                .HasColumnType("datetime")
                .HasColumnName("ORDER_DATE");
            entity.Property(e => e.PtThanhtoan)
                .HasMaxLength(50)
                .HasColumnName("PT_THANHTOAN");
            entity.Property(e => e.TotalPrice)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("TOTAL_PRICE");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__OrderDet__3214EC2781B8D563");

            entity.ToTable("OrderDetail");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.IdOrder).HasColumnName("ID_ORDER");
            entity.Property(e => e.IdProd).HasColumnName("ID_PROD");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRICE");
            entity.Property(e => e.Quantity).HasColumnName("QUANTITY");
            entity.Property(e => e.Sale)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("SALE");

            entity.HasOne(d => d.IdOrderNavigation).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Order");

            entity.HasOne(d => d.IdOrder1).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.IdOrder)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetail_Product");
        });

        modelBuilder.Entity<Producer>(entity =>
        {
            entity.HasKey(e => e.IdProducer).HasName("PK__Producer__88BD7532785392CC");

            entity.ToTable("Producer");

            entity.Property(e => e.IdProducer).HasColumnName("ID_PRODUCER");
            entity.Property(e => e.Address)
                .HasMaxLength(150)
                .HasColumnName("ADDRESS");
            entity.Property(e => e.Email)
                .HasMaxLength(150)
                .HasColumnName("EMAIL");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.Img)
                .HasMaxLength(150)
                .HasColumnName("IMG");
            entity.Property(e => e.PhoneNums)
                .HasMaxLength(50)
                .HasColumnName("PHONE_NUMS");
            entity.Property(e => e.ProducerName)
                .HasMaxLength(150)
                .HasColumnName("PRODUCER_NAME");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.IdProd).HasName("PK__Product__BA497EFEC97E67E7");

            entity.ToTable("Product");

            entity.Property(e => e.IdProd)
                .ValueGeneratedNever()
                .HasColumnName("ID_PROD");
            entity.Property(e => e.AliasName)
                .HasMaxLength(150)
                .HasColumnName("ALIAS_NAME");
            entity.Property(e => e.Description)
                .HasMaxLength(150)
                .HasColumnName("DESCRIPTION");
            entity.Property(e => e.Hide).HasColumnName("HIDE");
            entity.Property(e => e.IdCate).HasColumnName("ID_CATE");
            entity.Property(e => e.IdProducer).HasColumnName("ID_PRODUCER");
            entity.Property(e => e.Img1)
                .HasMaxLength(300)
                .HasColumnName("IMG1");
            entity.Property(e => e.Img2)
                .HasMaxLength(300)
                .HasColumnName("IMG2");
            entity.Property(e => e.Img3)
                .HasMaxLength(300)
                .HasColumnName("IMG3");
            entity.Property(e => e.Link)
                .HasMaxLength(300)
                .HasColumnName("LINK");
            entity.Property(e => e.Nsx)
                .HasColumnType("datetime")
                .HasColumnName("NSX");
            entity.Property(e => e.Nums).HasColumnName("NUMS");
            entity.Property(e => e.Price)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("PRICE");
            entity.Property(e => e.ProdName)
                .HasMaxLength(150)
                .HasColumnName("PROD_NAME");
            entity.Property(e => e.Sale)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("SALE");
            entity.Property(e => e.Unit)
                .HasMaxLength(50)
                .HasColumnName("UNIT");

            entity.HasOne(d => d.IdCateNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdCate)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Category");

            entity.HasOne(d => d.IdProducerNavigation).WithMany(p => p.Products)
                .HasForeignKey(d => d.IdProducer)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Product_Producer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
