using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.ClassBL
{
    public class CourseBL
    {
        LearningDbContext Context ;

        public List<Course> GetAll()
        {
            return Context.Courses.Include(c => c.Departments).ToList();
        }

        public Course GetById(int id)
        {
            return Context.Courses.Include(c => c.Departments)
                                  .Include(c => c.Teachers)
                                  .FirstOrDefault(c => c.Id == id);
        }

        public void Add(Course course)
        {
            Context.Courses.Add(course);
            Context.SaveChanges();
        }

        public void UpdateDB(Course updatedCourse)
        {
            Context.Courses.Update(updatedCourse);
            Context.SaveChanges();
        }

        public void DeleteDB(Course course)
        {
            Context.Courses.Remove(course);
            Context.SaveChanges();
        }
    }
}