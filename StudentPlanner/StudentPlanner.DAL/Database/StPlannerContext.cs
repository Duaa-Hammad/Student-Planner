using Microsoft.EntityFrameworkCore;
using StudentPlanner.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentPlanner.DAL.Database
{
    public class StPlannerContext: DbContext
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

            // Assignment - Reminder (one-to-many, optional)
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Assignment)
                .WithMany(a => a.Reminders)
                .HasForeignKey(r => r.AssignmentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Exam - Reminder (one-to-many, optional)
            modelBuilder.Entity<Reminder>()
                .HasOne(r => r.Exam)
                .WithMany(e => e.Reminders)
                .HasForeignKey(r => r.ExamId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
}
