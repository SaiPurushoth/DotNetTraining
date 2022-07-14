using Microsoft.AspNetCore.Mvc;
using MVCClientApp.Models;

namespace MVCClientApp.Controllers
{
    [Route("{controller}")]
    public class EmployeeController : Controller
    {
        [Route("api/index")]
        public IActionResult Index()
        {
            return View();
        }

        [Route("api/show")]
        public IActionResult ShowEmployees()
        {
            List<EmployeeDetail> employee = display();
            return View(employee);
        }

        [HttpPost]
        [Route("api/save")]
        public IActionResult SaveEmployees(EmployeeDetail model)
        {

            List<EmployeeDetail> employee = new List<EmployeeDetail>();

            EmployeeDetail emp = new EmployeeDetail();
            Random rnd = new Random();
            if (!ModelState.IsValid)
            {
                return null;
            }
            emp.Id=rnd.Next();
            emp.Name = model.Name;
            emp.Age = model.Age;
            emp.Phone = model.Phone;
         
            employee.Add(emp);
            return View(employee);
        }

        private List<EmployeeDetail> display()
        {
            List<EmployeeDetail> employees = new List<EmployeeDetail>();
            EmployeeDetail emp1 = new();
            emp1.Id = 1;
            emp1.Age = 18;
            emp1.Name = "sai";
            emp1.Phone = "9677558288";
            employees.Add(emp1);
            EmployeeDetail emp2 = new();
            emp2.Id = 2;
            emp2.Age = 20;
            emp2.Name = "rahul";
            emp2.Phone = "9677334572";
            employees.Add(emp2);
            EmployeeDetail emp3 = new();
            emp3.Id = 3;
            emp3.Age = 22;
            emp3.Name = "sriram";
            emp3.Phone = "9677459188";
            employees.Add(emp3);

            return employees;

        }
       
    }
}
