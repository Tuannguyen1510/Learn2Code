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

    public virtual DbSet<Category> Categorys { get; set; }

    public virtual DbSet<Course> Courses { get; set; }

    public virtual DbSet<Enrollment> Enrollments { get; set; }

    public virtual DbSet<Lession> Lessions { get; set; }

    public virtual DbSet<Moderator> Moderators { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Section> Sections { get; set; }

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

        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.Idcategory);

            entity.Property(e => e.Idcategory).HasColumnName("IDCategory");
            entity.Property(e => e.CategoryDesc).IsUnicode(false);
            entity.Property(e => e.CategoryName).IsUnicode(false);
        });

        modelBuilder.Entity<Course>(entity =>
        {
            entity.HasKey(e => e.Idcourse);

            entity.Property(e => e.Idcourse).HasColumnName("IDCourse");
            entity.Property(e => e.CourseDesc).IsUnicode(false);
            entity.Property(e => e.CourseName).IsUnicode(false);
            entity.Property(e => e.Idcategory).HasColumnName("IDCategory");

            entity.HasOne(d => d.IdcategoryNavigation).WithMany(p => p.Courses)
                .HasForeignKey(d => d.Idcategory)
                .HasConstraintName("FK_Courses_Categorys");
        });

        modelBuilder.Entity<Enrollment>(entity =>
        {
            entity.HasKey(e => e.Idenroll);

            entity.ToTable("Enrollment");

            entity.Property(e => e.Idenroll).HasColumnName("IDEnroll");
            entity.Property(e => e.EnrollDate).IsUnicode(false);
            entity.Property(e => e.Idcourse).HasColumnName("IDCourse");
            entity.Property(e => e.Iduser).HasColumnName("IDUser");

            entity.HasOne(d => d.IdcourseNavigation).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Idcourse)
                .HasConstraintName("FK_Enrollment_Courses");

            entity.HasOne(d => d.IduserNavigation).WithMany(p => p.Enrollments)
                .HasForeignKey(d => d.Iduser)
                .HasConstraintName("FK_Enrollment_Users");
        });

        modelBuilder.Entity<Lession>(entity =>
        {
            entity.HasKey(e => e.Idlession);

            entity.ToTable("Lession");

            entity.Property(e => e.Idlession).HasColumnName("IDLession");
            entity.Property(e => e.Idcourse).HasColumnName("IDCourse");
            entity.Property(e => e.LessionDesc).IsUnicode(false);
            entity.Property(e => e.Sort).IsUnicode(false);
            entity.Property(e => e.Titel).IsUnicode(false);

            entity.HasOne(d => d.IdcourseNavigation).WithMany(p => p.Lessions)
                .HasForeignKey(d => d.Idcourse)
                .HasConstraintName("FK_Lession_Courses");
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

        modelBuilder.Entity<Section>(entity =>
        {
            entity.HasKey(e => e.Idsection);

            entity.ToTable("Section");

            entity.Property(e => e.Idsection).HasColumnName("IDSection");
            entity.Property(e => e.Idlession).HasColumnName("IDLession");
            entity.Property(e => e.Image)
                .IsUnicode(false)
                .HasColumnName("image");
            entity.Property(e => e.Titel).IsUnicode(false);
            entity.Property(e => e.TxtContent)
                .IsUnicode(false)
                .HasColumnName("txtContent");

            entity.HasOne(d => d.IdlessionNavigation).WithMany(p => p.Sections)
                .HasForeignKey(d => d.Idlession)
                .HasConstraintName("FK_Section_Lession");
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
