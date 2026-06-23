using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey.RepoCourses
{
    public interface ICourseRepo
    {
        void Add(Course course);
        void Update(Course course);
        void Delete(Course course);
        void Save();

        List<Course> GetAll();
        List<Course> GetAllWithDepartments();
        Course? GetById(int id);
        Course? GetByIdWithDepartment(int id);
        bool Exists(int id);

        List<Department> GetAllDepartments();
    }
}
