using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey
{
    public class StudentRepo : IstudentRepo
    {
        LearningDbContext _context;
        public StudentRepo(LearningDbContext context)
        {
            _context = context;
        }
        // 2-CRUD operations
        public void Add(Student std)
        {
            _context.Add(std);
        }
        public void Update(Student std)
        {
            _context.Update(std); // modified
        }
        public void Delete(Student std)
        {
            _context.Remove(std);
        }
        public void Save()
        {
            _context.SaveChanges();
        }
        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }
        public Student GetById(int id)
        {
            return _context.Students.FirstOrDefault(d => d.Id == id);
        }
        // 3- Extra
        public Student GetByIdWithLoading(int id)
        {
            return _context.Students.Include(S => S.Department).FirstOrDefault(d => d.Id == id);
        }
        
        public List<Department> GetAllDepartments()
        {
            return _context.Departments.ToList();
        }
    }
}
