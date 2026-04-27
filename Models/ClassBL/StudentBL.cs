using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;

namespace Task_Day_2_ASP.Models.ClassBL
{
    public class StudentBL
    {
        LearningDbContext Context ;

        public List<Student> GetAll()
        {
            return Context.Students.ToList();
        }

        public Student GetById(int id)
        {
            return Context.Students.FirstOrDefault((D) => D.Id == id ); ;
        }

        public void UpdateDB(Student updateStd)
        {
            Context.Students.Update(updateStd);
            Context.SaveChanges();
        }

        public void DeleteDB(Student removeStd)
        {
            Context.Students.Remove(removeStd);
        }

       
       
        public void Add(Student Std)
        {
           
            Context.Students.Add(Std);
            Context.SaveChanges();
        }


    }
}
