using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey.RepoDepaatment
{
    public class DepartmentRepo : IDepartmentRepo
    {
        private readonly LearningDbContext _context;

        public DepartmentRepo(LearningDbContext context)
        {
            _context = context;
        }

        public void Add(Department department)
        {
            _context.Add(department);
        }
        public void Update(Department department)
        {
            _context.Update(department);
        }
        public void Delete(Department department)
        {
            _context.Remove(department);
        }
        public void Save() { _context.SaveChanges(); }


        public List<Department> GetAll()
        {
         return  _context.Departments.ToList();
        }

        public Department? GetById(int id)
        {
            return _context.Departments.FirstOrDefault(d => d.Id == id);
        }
        public Department? GetByIdWithDetails(int id)
        {
            return _context.Departments
                .Include(d => d.Students)
                .Include(d => d.Courses)
                .Include(d => d.Teachers)
                .FirstOrDefault(d => d.Id == id);
        }
        public bool Exists(int id)
            => _context.Departments.Any(d => d.Id == id);
    }
}
