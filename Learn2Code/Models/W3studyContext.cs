using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Learn2Code.Models;

public partial class W3studyContext : DbContext
{
    public W3studyContext()
    {
    }

    public W3studyContext(DbContextOptions<W3studyContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Admin> Admins { get; set; }

    public virtual DbSet<Moderator> Moderators { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=SUPERCOMPUTER\\SUPERCOMPUTER;Database=W3Study;uid=sa;pwd=123456;MultipleActiveResultSets=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Admin>(entity =>
        {
            entity.HasKey(e => e.Idadmin);

            entity.ToTable("Admin");

            entity.Property(e => e.Idadmin).HasColumnName("IDAdmin");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Admins)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_Admin_Users");
        });

        modelBuilder.Entity<Moderator>(entity =>
        {
            entity.HasKey(e => e.Idmoderator);

            entity.ToTable("Moderator");

            entity.Property(e => e.Idmoderator).HasColumnName("IDModerator");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Moderators)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_Moderator_Users");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.Idrole);

            entity.Property(e => e.Idrole)
                .ValueGeneratedNever()
                .HasColumnName("IDRole");
            entity.Property(e => e.Description)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.NameRole)
                .HasMaxLength(250)
                .IsFixedLength();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Iduser);

            entity.Property(e => e.Iduser).HasColumnName("IDUser");
            entity.Property(e => e.Avatar)
                .HasMaxLength(255)
                .IsFixedLength();
            entity.Property(e => e.Contact).IsUnicode(false);
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.Idrole).HasColumnName("IDRole");
            entity.Property(e => e.PassWord)
                .HasMaxLength(100)
                .IsFixedLength();
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .IsFixedLength();

            entity.HasOne(d => d.IdroleNavigation).WithMany(p => p.Users)
                .HasForeignKey(d => d.Idrole)
                .HasConstraintName("FK_Users_Roles");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
