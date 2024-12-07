using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DBLaba6.MainDB;

public partial class OpendatamodelContext : DbContext
{
    public OpendatamodelContext()
    {
    }

    public OpendatamodelContext(DbContextOptions<OpendatamodelContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Access> Accesses { get; set; }

    public virtual DbSet<Categoty> Categoties { get; set; }

    public virtual DbSet<Datum> Data { get; set; }

    public virtual DbSet<Link> Links { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Tag> Tags { get; set; } 

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;port=3306;database=opendatamodel;uid=root;password=Rgrichell24", Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.35-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_unicode_520_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Access>(entity =>
        {
            entity.HasKey(e => new { e.AccessId, e.DataId, e.UserId, e.RoleId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0, 0 });

            entity.ToTable("access");

            entity.HasIndex(e => e.DataId, "fk_Access_Data_idx");

            entity.HasIndex(e => e.RoleId, "fk_Access_Role_idx");

            entity.HasIndex(e => e.UserId, "fk_Access_User_idx");

            entity.Property(e => e.AccessId)
                .ValueGeneratedOnAdd()
                .HasColumnName("access_id");
            entity.Property(e => e.DataId).HasColumnName("data_id");
            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.RoleId).HasColumnName("role_id");

            entity.HasOne(d => d.Data).WithMany(p => p.Accesses)
                .HasForeignKey(d => d.DataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Access_Data");

            entity.HasOne(d => d.Role).WithMany(p => p.Accesses)
                .HasForeignKey(d => d.RoleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Access_Role");
        });

        modelBuilder.Entity<Categoty>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categoty");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.HasIndex(e => e.ParentCategoryId, "parent_category_idx");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.ParentCategoryId).HasColumnName("parent_category_id");

            entity.HasOne(d => d.ParentCategory).WithMany(p => p.InverseParentCategory)
                .HasForeignKey(d => d.ParentCategoryId)
                .HasConstraintName("parent_category");
        });

        modelBuilder.Entity<Datum>(entity =>
        {
            entity.HasKey(e => e.DataId).HasName("PRIMARY");

            entity.ToTable("data");

            entity.HasIndex(e => e.CategoryId, "fk_Data_Categoty_idx");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.DataId).HasColumnName("data_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Content)
                .HasMaxLength(45)
                .HasColumnName("content");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Format)
                .HasMaxLength(45)
                .HasColumnName("format");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
            entity.Property(e => e.UpdatedAt)
                .HasColumnType("datetime")
                .HasColumnName("updatedAt");

            entity.HasOne(d => d.Category).WithMany(p => p.Data)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Data_Categoty");
        });

        modelBuilder.Entity<Link>(entity =>
        {
            entity.HasKey(e => new { e.LinkId, e.DataId, e.TagId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0, 0 });

            entity.ToTable("link");

            entity.HasIndex(e => e.DataId, "fk_Link_Data_idx");

            entity.HasIndex(e => e.TagId, "fk_Link_Tag_idx");

            entity.Property(e => e.LinkId)
                .ValueGeneratedOnAdd()
                .HasColumnName("link_id");
            entity.Property(e => e.DataId).HasColumnName("data_id");
            entity.Property(e => e.TagId).HasColumnName("tag_id");

            entity.HasOne(d => d.Data).WithMany(p => p.Links)
                .HasForeignKey(d => d.DataId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Link_Data");

            entity.HasOne(d => d.Tag).WithMany(p => p.Links)
                .HasForeignKey(d => d.TagId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Link_Tag");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("PRIMARY");

            entity.ToTable("role");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.RoleId).HasColumnName("role_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Tag>(entity =>
        {
            entity.HasKey(e => e.TagId).HasName("PRIMARY");

            entity.ToTable("tag");

            entity.HasIndex(e => e.Name, "name_UNIQUE").IsUnique();

            entity.Property(e => e.TagId).HasColumnName("tag_id");
            entity.Property(e => e.Name)
                .HasMaxLength(45)
                .HasColumnName("name");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user");

            entity.HasIndex(e => e.Email, "email_UNIQUE").IsUnique();

            entity.HasIndex(e => e.Login, "login_UNIQUE").IsUnique();

            entity.Property(e => e.UserId).HasColumnName("user_id");
            entity.Property(e => e.Email)
                .HasMaxLength(45)
                .HasColumnName("email");
            entity.Property(e => e.Firstname)
                .HasMaxLength(45)
                .HasColumnName("firstname");
            entity.Property(e => e.Lastname)
                .HasMaxLength(45)
                .HasColumnName("lastname");
            entity.Property(e => e.Login)
                .HasMaxLength(45)
                .HasColumnName("login");
            entity.Property(e => e.Password)
                .HasMaxLength(45)
                .HasColumnName("password");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
