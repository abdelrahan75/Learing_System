using Task_Day_2_ASP.Models.Entities;
using Task_Day_2_ASP.Models.ViewModel;

namespace Task_Day_2_ASP.Models.Reposiotoriey.RepoStudent
{
    public interface IstudentRepo
    {
        
        void Add(Student std);
        void Update(Student std);
        void Delete(Student std);
        void Save();

       
        List<Student> GetAll();
        Student GetById(int id);
        Student GetByIdWithLoading(int id);

        // Related data
        List<Department> GetAllDepartments();
        Course GetCourseById(int id);
        StuCrsRes GetStudentCourseResult(int studentId, int courseId);
    }
}
