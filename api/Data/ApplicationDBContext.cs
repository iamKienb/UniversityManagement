using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Entity;
using Microsoft.EntityFrameworkCore;

namespace api.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<Student>()
                .HasOne(s => s.Major)
                .WithMany(m => m.Students)
                .HasForeignKey(s => s.MajorId);

            modelBuilder.Entity<Education>()
                .HasOne(e => e.Student)
                .WithMany()
                .HasForeignKey(e => e.StudentId);

            modelBuilder.Entity<NewsFeed>()
                .HasOne(n => n.Student)
                .WithMany()
                .HasForeignKey(n => n.StudentId)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ExamSchedule>()
                .HasOne(es => es.Student)
                .WithMany()
                .HasForeignKey(es => es.StudentId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<ExamSchedule>()
                .HasOne(es => es.Subject)
                .WithMany()
                .HasForeignKey(es => es.SubjectId)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Student)  // Một sinh viên có nhiều môn học thông qua bảng trung gian
                .WithMany()
                .HasForeignKey(ss => ss.StudentId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<StudentSubject>()
                .HasOne(ss => ss.Subject)  // Một môn học có nhiều sinh viên thông qua bảng trung gian
                .WithMany(s => s.StudentSubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .OnDelete(DeleteBehavior.Restrict);

            base.OnModelCreating(modelBuilder);
        }

        public virtual DbSet<Student> Student { get; set; }
        public virtual DbSet<Major> Major { get; set; }
        public virtual DbSet<Education> Education { get; set; }

        public virtual DbSet<NewsFeed> NewsFeed { get; set; }

        public virtual DbSet<Subject> Subject { get; set; }

        public virtual DbSet<ExamSchedule> ExamSchedule { get; set; }
        
        public virtual DbSet<StudentSubject> StudentSubject { get; set; }

    }
}