using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.ClassBL;
using Task_Day_2_ASP.Models.Entities;
using Task_Day_2_ASP.Models.Reposiotoriey;
using Task_Day_2_ASP.Models.ViewModel;

namespace Task_Day_2_ASP.Controllers
{
    public class StudentsController : Controller
    {
        //private readonly LearningDbContext _context;
        IstudentRepo _context;

        public StudentsController(IstudentRepo istudentRepo)
        {
            _context = istudentRepo;
        }

        // GET: Students
        public IActionResult Index()
        {
             
            return View("Index",_context.GetAll());
        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student student = _context.GetById((int)id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.GetAll(), "Id", "MgrName");
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Age,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                 _context.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.GetAll(), "Id", "MgrName", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student student =  _context.GetByIdWithLoading((int)id);
            if (student == null)
            {
                return NotFound();
            }
            ViewData["DepartmentId"] = new SelectList(_context.GetAll(), "Id", "MgrName", student.DepartmentId);
            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  IActionResult Edit(int id, [Bind("Id,Name,Age,DepartmentId")] Student student)
        {
            if (id != student.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                     _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.GetAll(), "Id", "MgrName", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = _context.GetById((int)id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var student = _context.GetById(id);
            if (student != null)
            {
                _context.Delete(student);
            }

             _context.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
           if (_context.GetById(id) is Student)
            {
                return true;
            }
           else return false;
        }

        public IActionResult ShowResult(int StuId,int CrsId)
        {
            Student std = _context.GetById(StuId);
          //  Course Crs = _context.Courses.FirstOrDefault(c=>c.Id==CrsId);
          //  StuCrsRes stuCourse = _context.StuCrsRes
      //  .FirstOrDefault(sc => sc.StudentId == StuId && sc.CourseId == CrsId);

            StudentCourseResultViewModel  vm = new StudentCourseResultViewModel();

            vm.StudentName = std.Name;
           // vm.CourseName = Crs.Name;
           // vm.Degree = stuCourse.Degree;

          //  vm.Color = stuCourse.Degree >= Crs.MinDegree ? "green" : "red";
            return View(vm);


        }
    }
}
