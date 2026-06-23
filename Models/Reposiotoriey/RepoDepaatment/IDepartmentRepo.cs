using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.Reposiotoriey.RepoDepaatment
{
    public interface IDepartmentRepo
    {
        void Add(Department department);
        void Update(Department department);
        void Delete(Department department);
        void Save();

        List<Department> GetAll();
        Department? GetById(int id);
        Department? GetByIdWithDetails(int id);
        bool Exists(int id);
    }
}
