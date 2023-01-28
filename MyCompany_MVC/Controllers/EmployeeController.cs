using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany_MVC.Models;

namespace MyCompany_MVC.Controllers
{
    public class EmployeeController : Controller
    {
        private CompanyDBContext DB;
        public EmployeeController()
        {
            DB = new CompanyDBContext();
        }
        public IActionResult Index()
        {

            List<Employee> employees = DB.Employees.ToList();
            return View("Index", employees);
        }

        public IActionResult GetById(int? id)
        {


            Employee? employee = DB.Employees.Include(s => s.Supervisor).Include(e => e.DepartmentManege).Include(e => e.DepartmentManege).Where(e => e.SSN == id).SingleOrDefault();
            if (employee == null)
                return View("Error");
            else
                return View("GetById", employee);
        }

        public IActionResult Add()
        {
            List<Employee> employees = DB.Employees.ToList();
            return View(employees);
        }

        public IActionResult AddEmployeeDb(Employee employee)
        {
            DB.Employees.Add(employee);
            DB.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Update(int id)
        {
            List<Employee> employees = DB.Employees.ToList();
            Employee? employee = DB.Employees.Include(s => s.Supervisor).Where(e => e.SSN == id).SingleOrDefault();
            ViewBag.emp = employees;
            if (employee == null)
                return View("Error");
            else
                return View(employee);
        }

        public IActionResult UpdateDB(Employee employee)
        {
            Employee oldEmployee = DB.Employees.Where(e => e.SSN == employee.SSN).SingleOrDefault();
            oldEmployee.Fname = employee.Fname;
            oldEmployee.Lname = employee.Lname;
            oldEmployee.Minit = employee.Minit;
            oldEmployee.Address = employee.Address;
            oldEmployee.Salary = employee.Salary;
            oldEmployee.Sex = employee.Sex;
            oldEmployee.SupervisorSSN = employee.SupervisorSSN;
            oldEmployee.BirthDate = employee.BirthDate;

            DB.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Delete(int id)
        {

            Employee Employee = DB.Employees.Where(e => e.SSN == id).SingleOrDefault();
            DB.Employees.Remove(Employee);
            DB.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Login()
        {
            return View();
        }
        public IActionResult LoginDB(Employee employeeTologin)
        {

            Employee employee = DB.Employees.SingleOrDefault(e => e.SSN == employeeTologin.SSN && e.Fname == employeeTologin.Fname);

            if (employee == null)
                return View("Error");
            else
            {
                HttpContext.Session.SetInt32("SSN", employee.SSN);
                return GetById(employee.SSN);
            }

        }

        public IActionResult AllEmployeeManger()
        {

            List<Employee>? employees = DB.Departments.Include(e => e.employeeManege).Where(e => e.mngrSSN != null).Select(e => e.employeeManege).ToList();

            return View(employees);

        }

    }
}
