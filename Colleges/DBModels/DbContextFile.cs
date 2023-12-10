using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Colleges.DBModels
{
    public partial class DbContextFile : DbContext
    {
        public DbContextFile()
        {
        }

        public DbContextFile(DbContextOptions<DbContextFile> options)
            : base(options)
        {
        }

        public virtual DbSet<Course> Courses { get; set; } = null!;
        public virtual DbSet<CourseUniversity> CourseUniversities { get; set; } = null!;
        public virtual DbSet<Student> Students { get; set; } = null!;
        public virtual DbSet<StudentCourse> StudentCourses { get; set; } = null!;
        public virtual DbSet<StudentUniversity> StudentUniversities { get; set; } = null!;
        public virtual DbSet<University> Universities { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>(entity =>
            {
                entity.ToTable("Course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<CourseUniversity>(entity =>
            {
                entity.ToTable("Course_University");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.CourseUniversities)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_University_Course");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.CourseUniversities)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Course_University_University");
            });

            modelBuilder.Entity<Student>(entity =>
            {
                entity.ToTable("Student");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            modelBuilder.Entity<StudentCourse>(entity =>
            {
                entity.ToTable("Student_Course");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.CidNavigation)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.Cid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Course_Course");

                entity.HasOne(d => d.SidNavigation)
                    .WithMany(p => p.StudentCourses)
                    .HasForeignKey(d => d.Sid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_Course_Student");
            });

            modelBuilder.Entity<StudentUniversity>(entity =>
            {
                entity.ToTable("Student_University");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.HasOne(d => d.SidNavigation)
                    .WithMany(p => p.StudentUniversities)
                    .HasForeignKey(d => d.Sid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_University_Student");

                entity.HasOne(d => d.UidNavigation)
                    .WithMany(p => p.StudentUniversities)
                    .HasForeignKey(d => d.Uid)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Student_University_University");
            });

            modelBuilder.Entity<University>(entity =>
            {
                entity.ToTable("University");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name).HasMaxLength(150);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
