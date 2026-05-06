using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Task_Day_2_ASP.Models.Entities;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoTeachers;

namespace Task_Day_2_ASP.Controllers
{
    public class TeachersController : Controller
    {
        private readonly ITeacherRepo _repo;

        public TeachersController(ITeacherRepo repo)
        {
            _repo = repo;
        }

        // GET: Teachers
        public IActionResult Index()
        {
            return View(_repo.GetAllWithDetails());
        }

        // GET: Teachers/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            Teacher teacher = _repo.GetByIdWithDetails((int)id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }

        // GET: Teachers/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name");
            ViewData["CourseId"] = new SelectList(_repo.GetAllCourses(), "Id", "Name");
            return View();
        }

        // POST: Teachers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Salary,CourseId,DepartmentId")] Teacher teacher)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(teacher);
                _repo.Save();
                TempData["NotificationAdded"] = $"Teacher '{teacher.Name}' was added successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name", teacher.DepartmentId);
            ViewData["CourseId"] = new SelectList(_repo.GetAllCourses(), "Id", "Name", teacher.CourseId);
            return View(teacher);
        }

        // GET: Teachers/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Teacher teacher = _repo.GetById((int)id);
            if (teacher == null) return NotFound();

            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name", teacher.DepartmentId);
            ViewData["CourseId"] = new SelectList(_repo.GetAllCourses(), "Id", "Name", teacher.CourseId);
            return View(teacher);
        }

        // POST: Teachers/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Salary,CourseId,DepartmentId")] Teacher teacher)
        {
            if (id != teacher.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(teacher);
                    _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.Exists(teacher.Id)) return NotFound();
                    throw;
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name", teacher.DepartmentId);
            ViewData["CourseId"] = new SelectList(_repo.GetAllCourses(), "Id", "Name", teacher.CourseId);
            return View(teacher);
        }

        // GET: Teachers/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            Teacher teacher = _repo.GetByIdWithDetails((int)id);
            if (teacher == null) return NotFound();

            return View(teacher);
        }

        // POST: Teachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Teacher teacher = _repo.GetById(id);
            if (teacher != null)
            {
                _repo.Delete(teacher);
                _repo.Save();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
