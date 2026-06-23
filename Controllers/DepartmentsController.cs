using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Task_Day_2_ASP.Data.Dbcontext;
using Task_Day_2_ASP.Models.Entities;
using Task_Day_2_ASP.Models.Reposiotoriey.RepoDepaatment;

namespace Task_Day_2_ASP.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly IDepartmentRepo _repo;

        public DepartmentsController(IDepartmentRepo repo)
        {
            _repo = repo;
        }

        // GET: Departments
        public IActionResult Index()
        {
            return View(_repo.GetAll());
        }

        // GET: Departments/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null) return NotFound();

            Department? department = _repo.GetByIdWithDetails((int)id);
            if (department == null) return NotFound();

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Departments/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,Name,MgrName")] Department department)
        {
            if (ModelState.IsValid)
            {
                _repo.Add(department);
                _repo.Save();
                TempData["NotificationAdded"] = $"Department '{department.Name}' was added successfully.";
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null) return NotFound();

            Department? department = _repo.GetById((int)id);
            if (department == null) return NotFound();

            return View(department);
        }

        
        // POST: Departments/Edit/5
      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Name,MgrName")] Department department)
        {
            if (id != department.Id) return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    _repo.Update(department);
                    _repo.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repo.Exists(department.Id)) return NotFound();
                    throw;
                }
            
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null) return NotFound();

            Department? department = _repo.GetById((int)id);
            if (department == null) return NotFound();

            return View(department);
        }

        // POST: Departments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Department? department = _repo.GetById(id);
            if (department != null)
            {
                _repo.Delete(department);
                _repo.Save();
            }
        
            
            return RedirectToAction(nameof(Index));
        }


    }
}
