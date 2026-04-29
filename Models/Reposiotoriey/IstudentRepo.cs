using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey
{
    public interface IstudentRepo
    {
        public void Add(Student std);
        public void Update(Student std);
        public void Delete(Student std);
        public void Save();
        public List<Student> GetAll();
        public Student GetById(int id);
        public Student GetByIdWithLoading(int id);

        public List<Department> GetAllDepartments();
    }
}
