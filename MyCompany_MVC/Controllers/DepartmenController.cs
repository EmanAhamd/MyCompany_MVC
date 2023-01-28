using Microsoft.AspNetCore.Mvc;
using MyCompany_MVC.Models;
using Microsoft.EntityFrameworkCore;

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
            return View();
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
