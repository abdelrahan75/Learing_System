using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoStudent;
using Task_Day_2_ASP.Models.ViewModel;

namespace Task_Day_2_ASP.Controllers
{
    public class StudentsController : Controller
    {
       
        IstudentRepo _context;

        public StudentsController(IstudentRepo istudentRepo)
        {
            _context = istudentRepo;
        }

        // GET: Students
        public IActionResult Index()
        {

            return View(_context.GetAll());
        }

        // GET: Students/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Student student = _context.GetByIdWithLoading((int)id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        public IActionResult DetailsOfVM(int id)
        {
            Student student = _context.GetByIdWithLoading(id);
            if (student == null) return NotFound();

            var vm = new StudentViewModel
            {
                Id = student.Id,
                Name = student.Name,
                Age = student.Age,
                DepartmentId = student.DepartmentId,
                DepartmentName = student.Department?.Name
            };

            return View("DetailsOfVm", vm);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_context.GetAllDepartments(), "Id", "Name");
            return View();
        }

        // POST: Students/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Age,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Add(student);
                _context.Save();
                TempData["NotificationAdded"] = $"student with id {student.Id} and Name {student.Name} is Added";
                return RedirectToAction(nameof(Index));
            }
            // ✅ Fixed: GetAllDepartments() + consistent "Name" field
            ViewData["DepartmentId"] = new SelectList(_context.GetAllDepartments(), "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Student student = _context.GetByIdWithLoading((int)id);
            if (student == null) return NotFound();

            ViewData["DepartmentId"] = new SelectList(_context.GetAllDepartments(), "Id", "Name", student.DepartmentId);
            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Age,DepartmentId")] Student student)
        {
            if (id != student.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    _context.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (_context.GetById(student.Id) == null) return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_context.GetAllDepartments(), "Id", "Name", student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public IActionResult Delete(int? id)
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

        public IActionResult ShowResult(int StuId, int CrsId)
        {
            Student student = _context.GetById(StuId);
            Course course = _context.GetCourseById(CrsId);
            StuCrsRes result = _context.GetStudentCourseResult(StuId, CrsId);

            if (student == null || course == null || result == null)
                return NotFound();

            var vm = new StudentCourseResultViewModel
            {
                StudentName = student.Name ?? string.Empty,
                CourseName = course.Name ?? string.Empty,
                Degree = result.Degree,
                Color = result.Degree >= course.MinDegree ? "green" : "red"
            };
            return View(vm);
      
  
        }
   }
}
