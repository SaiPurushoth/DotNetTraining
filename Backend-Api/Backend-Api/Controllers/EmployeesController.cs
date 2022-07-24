using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Backend_Api.Models;

namespace Backend_Api.Controllers
{
    [ApiController]
    [Route("{Controller}")]
    public class EmployeesController : Controller
    {
        private readonly EmployeesDbContext _context;

        public EmployeesController(EmployeesDbContext context)
        {
            _context = context;
        }

        // GET: Employees
        [Route("index")]
        public async Task<JsonResult> Index()
        {
              return _context.Employees != null ? 
                          Json(new { Employees = await _context.Employees.ToListAsync() }) :
                          Json(new {Message = "Entity set 'EmployeesDbContext.Employees'  is null." });
        }


        // GET: Employees/Details/5
        [Route("details")]
        public async Task<JsonResult> Details(int? id)
        {
            if (id == null || _context.Employees == null)
            {
                return Json(new { Message = "Not Found" });
            }

            var employees = await _context.Employees
                .FirstOrDefaultAsync(m => m.Id == id);
            if (employees == null)
            {
       
                return Json(new { Message= "Not Found" });
            }

            return Json(new { employees });
        }


        // POST: Employees/Create
        [HttpPost]
        [Route("create")]
        public async Task<JsonResult> Create(int id,string name,int age,string phone)
        {
            Employees employees = new();
            employees.Id = id;
              employees.Name = name;
            employees.Age = age;
            employees.Phone = phone;

            if (ModelState.IsValid)
            {
                _context.Add(employees);
                await _context.SaveChangesAsync();
                return Json(new { Message= "successfully done" });
            }
            return Json(new { employees });
        }

  

        // POST: Employees/Edit/5
        [HttpPost]
        [Route("edit")]
        public async Task<JsonResult> Edit(int id, string name, int age, string phone)
        {
            Employees employees = new();
            employees.Id = id;
            employees.Name = name;
            employees.Age = age;
            employees.Phone = phone;
            if (id != employees.Id)
            {
                return Json(new { Message = "Not Found" });
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employees);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeesExists(employees.Id))
                    {
                        return Json(new { Message = "Not Found" });
                    }
                    else
                    {
                        throw;
                    }
                }
                return Json(new { Message = "successfully done" });
            }
            return Json(new { employees });
        }



        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [Route("delete")]
        public async Task<JsonResult> DeleteConfirmed(int id)
        {
            if (_context.Employees == null)
            {
                return Json(new { Message = "Entity set 'EmployeesDbContext.Employees'  is null." });
            }
            var employees = await _context.Employees.FindAsync(id);
            if (employees != null)
            {
                _context.Employees.Remove(employees);
            }
            
            await _context.SaveChangesAsync();
            return Json(new { Message = "successfully done" });
        }

        private bool EmployeesExists(int id)
        {
          return (_context.Employees?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
