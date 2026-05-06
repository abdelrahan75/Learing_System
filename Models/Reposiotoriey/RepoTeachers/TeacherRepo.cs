using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey.RepoTeachers
{
    public class TeacherRepo : ITeacherRepo
    {
        private readonly LearningDbContext _context;

        public TeacherRepo(LearningDbContext context)
        {
            _context = context;
        }

        public void Add(Teacher teacher)
        {
            _context.Add(teacher);
        }
        public void Update(Teacher teacher)
        {
            _context.Update(teacher);
        }
        public void Delete(Teacher teacher)
        {
            _context.Remove(teacher);
        }
        public void Save()
        {
            _context.SaveChanges();
        }

        public List<Teacher> GetAll()
        { 
            return _context.Teachers.ToList();
        }

        public List<Teacher> GetAllWithDetails()
        {
           return _context.Teachers
                .Include(t => t.Course) 
                .Include(t => t.Department)
                .ToList();
        }

        public Teacher GetById(int id)
        { 
            return _context.Teachers.FirstOrDefault(t => t.Id == id);
        }

        public Teacher GetByIdWithDetails(int id)
        {
           return _context.Teachers
               .Include(t => t.Department)
               .Include(t => t.Course)
               .FirstOrDefault(t => t.Id == id);
        }

        public bool Exists(int id)
        {
            return _context.Teachers.Any(t => t.Id == id);
        }

        public List<Department> GetAllDepartments()
        {
           return _context.Departments.ToList();
        }

        public List<Course> GetAllCourses()
        { 
            return _context.Courses.ToList();
        }
    }
}
