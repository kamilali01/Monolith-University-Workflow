using Microsoft.EntityFrameworkCore;
using UniversityWorkflow.Api.Domain.Entities;

namespace UniversityWorkflow.Api.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Student> Students => Set<Student>();
        public DbSet<Course> Courses => Set<Course>();
        public DbSet<Enrollment> Enrollments => Set<Enrollment>();
        public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Student>()
                .HasIndex(x => x.Email)
                .IsUnique();

            modelBuilder.Entity<Course>()
                .HasIndex(x => x.Code)
                .IsUnique();

            modelBuilder.Entity<Enrollment>()
                .HasIndex(x => new { x.StudentId, x.CourseId })
                .IsUnique();
        }

    }
}
