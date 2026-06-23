using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey.RepoCourses
{
    public class CoursesRepo : ICourseRepo
    {
        private readonly LearningDbContext _context;

        public CoursesRepo(LearningDbContext context)
        {
            _context = context;
        }

        public void Add(Course course)
        {
            _context.Add(course);
        }
        public void Update(Course course)
        {
            _context.Update(course);
        }
        public void Delete(Course course)
        {
            _context.Remove(course);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Course> GetAll()
        { 
            return _context.Courses.ToList();
        }

        public List<Course> GetAllWithDepartments()
        { 
            return _context.Courses.Include(c => c.Departments).ToList();
        }
        public Course? GetById(int id)
        {
            return _context.Courses.FirstOrDefault(c => c.Id == id);
        }
        public Course? GetByIdWithDepartment(int id)
        {
            return _context.Courses.Include(c => c.Departments).FirstOrDefault(c => c.Id == id);
        }
        public bool Exists(int id)
        { 
            return _context.Courses.Any(c => c.Id == id);
        }
        public List<Department> GetAllDepartments()
        { 
            return _context.Departments.ToList();
        }
    }
}
