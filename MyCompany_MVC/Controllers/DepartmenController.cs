using Microsoft.AspNetCore.Mvc;
using MyCompany_MVC.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MyCompany_MVC.Controllers
{
    public class DepartmenController : Controller
    {
        private CompanyDBContext DB;

        public DepartmenController()
        {
            DB = new CompanyDBContext();

        }
        public IActionResult Index()
        {
            List<Department> departments = DB.Departments.Include(s => s.DepartmentLocations).Include(d => d.employeeManege).ToList();

            return View(departments);
        }
        ///
        public IActionResult GetDepartmentById(int id)
        {
            Department department = DB.Departments.Include(d => d.DepartmentLocations).Include(d => d.Projects).Include(d => d.employeeManege).SingleOrDefault(d => d.Number == id);
            if (department == null)
                return View("Error");
            else
                return View(department);
        }

        public IActionResult Add()
        {
            List<Employee> employees = DB.Employees.ToList();
            ViewBag.emps = new SelectList(employees, "SSN", "Fname");

            return View();
        }

        public IActionResult AddDb(Department department)
        {

            DB.Departments.Add(department);
            DB.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Edit(int id)
        {
            Department department = DB.Departments.Include(d => d.DepartmentLocations).SingleOrDefault(d => d.Number == id);
            List<Employee> employees = DB.Employees.ToList();
            ViewBag.emps = new SelectList(employees, "SSN", "Fname");


            return View(department);
        }

        public IActionResult EditDb(Department departmentToEdit)
        {

            DB.Departments.Update(departmentToEdit);
            DB.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Delete(int id)
        {
            Department department = DB.Departments.SingleOrDefault(d => d.Number == id);
            DB.Departments.Remove(department);
            DB.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult GetDepartmentManger( int id)
        {
            Department department = DB.Departments.Include(d => d.DepartmentLocations).Include(d => d.Projects).SingleOrDefault(d => d.mngrSSN == id);
            if (department == null)
                return View("Error");
            else
                return View("GetDepartmentManger", department);
        }
    }
}
