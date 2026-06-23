using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoCourses;

namespace Task_Day_2_ASP.Controllers
{
    public class CoursesController : Controller
    {
        private readonly ICourseRepo _repo;

        public CoursesController(ICourseRepo repo)
        {
            _repo = repo;
        }

        // GET: Courses
        public IActionResult Index()
        {
            return View(_repo.GetAllWithDepartments());
        }

        // GET: Courses/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            Course? course = _repo.GetByIdWithDepartment((int)id);
            if (course == null) return NotFound();

            return View(course);
        }

        // GET: Courses/Create
        public IActionResult Create()
        {
            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name");
            return View();
        }

        // POST: Courses/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,Degree,MinDegree,DepartmentId")] Course course)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(course);
                _repo.Save();
                TempData["NotificationAdded"] = $"Course '{course.Name}' was added successfully.";
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Course? course = _repo.GetById((int)id);
            if (course == null) return NotFound();

            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // POST: Courses/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,Degree,MinDegree,DepartmentId")] Course course)
        {
            if (id != course.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(course);
                    _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.Exists(course.Id)) return NotFound();
                    throw;
                }
               
                return RedirectToAction(nameof(Index));
            }
            ViewData["DepartmentId"] = new SelectList(_repo.GetAllDepartments(), "Id", "Name", course.DepartmentId);
            return View(course);
        }

        // GET: Courses/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            Course? course = _repo.GetByIdWithDepartment((int)id);
            if (course == null) return NotFound();

            return View(course);
        }

        // POST: Courses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Course? course = _repo.GetById(id);
            if (course != null)
            {
                _repo.Delete(course);
                _repo.Save();
            }
        
            
            return RedirectToAction(nameof(Index));
        }


    }
}
