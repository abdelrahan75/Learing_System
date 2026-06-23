using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey.RepoTeachers
{
    public interface ITeacherRepo
    {
        void Add(Teacher teacher);
        void Update(Teacher teacher);
        void Delete(Teacher teacher);
        void Save();

        List<Teacher> GetAll();
        List<Teacher> GetAllWithDetails();
        Teacher? GetById(int id);
        Teacher? GetByIdWithDetails(int id);
        bool Exists(int id);

        List<Department> GetAllDepartments();
        List<Course> GetAllCourses();
    }
}
