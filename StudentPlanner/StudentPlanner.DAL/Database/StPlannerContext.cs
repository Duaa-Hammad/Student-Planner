using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StudentPlanner.DAL.Entities;
using StudentPlanner.DAL.Extends;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Database
{
    public class StPlannerContext: IdentityDbContext<ApplicationUser>
    {
        public StPlannerContext(DbContextOptions<StPlannerContext> options) : base(options)
        {
        }
        public DbSet<Student> Students {  get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Exam> Exams { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Reminder> Reminders { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Course -> Assignments (Restrict to avoid cascade path)
            modelBuilder.Entity<Assignment>()
                .HasOne(a => a.Course)
                .WithMany(c => c.Assignments)
                .HasForeignKey(a => a.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Exams (Restrict to avoid cascade path)
            modelBuilder.Entity<Exam>()
                .HasOne(e => e.Course)
                .WithMany(c => c.Exams)
                .HasForeignKey(e => e.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Course -> Reminders (Restrict)
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Course)
                .WithMany(c => c.Reminders)
                .HasForeignKey(r => r.CourseId)
                .OnDelete(DeleteBehavior.Restrict);

            // Student -> Courses (cascade delete is OK)
            modelBuilder.Entity<Course>()
                .HasOne(r => r.Student)
                .WithMany(u => u.Courses)
                .HasForeignKey(r => r.StudentId)
                .OnDelete(DeleteBehavior.Cascade);

            // Assignment -> Reminders (SetNull)
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Assignment)
                .WithMany(a => a.Reminders)
                .HasForeignKey(r => r.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Exam -> Reminders (SetNull)
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Exam)
                .WithMany(e => e.Reminders)
                .HasForeignKey(r => r.ExamId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
