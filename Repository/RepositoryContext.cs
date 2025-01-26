using Entities.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Repository.Configuration;

namespace Repository
{
    public class RepositoryContext : IdentityDbContext<User>
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Student)
                .WithOne(s => s.User);

            modelBuilder.Entity<Student>()
                .HasOne(s => s.User)
                .WithOne(u => u.Student)
                .HasPrincipalKey<User>(u => u.Id);

            modelBuilder.Entity<User>()
                .HasOne(u => u.Lecturer)
                .WithOne(l => l.User);

            modelBuilder.Entity<Lecturer>()
                .HasOne(l => l.User)
                .WithOne(u => u.Lecturer)
                .HasPrincipalKey<User>(u => u.Id);

            modelBuilder.Entity<Course>()
                .HasOne(c => c.Level)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<ClassSchedule>()
                .Property(b => b.UpdatedAt)
                .HasDefaultValueSql("GETUTCDATE()");

            modelBuilder.ApplyConfiguration(new FacultyConfiguration());
            modelBuilder.ApplyConfiguration(new DepartmentConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new ClassroomConfiguration());
            modelBuilder.ApplyConfiguration(new LevelConfiguration());
            modelBuilder.ApplyConfiguration(new StudentConfiguration());
            modelBuilder.ApplyConfiguration(new LecturerConfiguration());
            modelBuilder.ApplyConfiguration(new CourseConfiguration());
        }

        public DbSet<AcademicSession> AcademicSessions { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Lecturer> Lecturers { get; set; }

        public DbSet<Faculty>? Faculties { get; set; }
        public DbSet<Department>? Departments { get; set; }
        public DbSet<Classroom>? Classrooms { get; set; }
        public DbSet<Course>? Courses { get; set; }
        public DbSet<ClassSchedule>? ClassSchedules { get; set; }
        public DbSet<Level>? Level { get; set; }
        public DbSet<Attendance>? Attendances { get; set; }
        public DbSet<Enrollment>? Enrollments { get; set; }
    }
}
