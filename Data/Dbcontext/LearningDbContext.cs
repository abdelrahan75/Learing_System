using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Data.Configures;
using Task_Day_2_ASP.Models.Entities;
namespace Task_Day_2_ASP.Data.Dbcontext
    
{
    public class LearningDbContext :DbContext
    {
        

        public LearningDbContext (DbContextOptions options) : base(options) { }

        public DbSet<Teacher> Teachers { get; set; }

        public DbSet<Student> Students { get; set; }

        public DbSet<StuCrsRes> StuCrsRes { get; set; }

        public DbSet<Course> Courses { get; set; } 

        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration( new StudentConfigure());
            modelBuilder.Entity<StuCrsRes>()
                .HasKey(s => new { s.StudentId, s.CourseId });

            // إضافة الـ precision للـ Salary
            modelBuilder.Entity<Teacher>()
                .Property(t => t.Salary)
                .HasPrecision(18, 2);
        }
    }
}
