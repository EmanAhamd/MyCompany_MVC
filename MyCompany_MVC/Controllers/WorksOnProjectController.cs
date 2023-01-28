using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany_MVC.Models;

namespace MyCompany_MVC.Controllers
{
    public class WorksOnProjectController : Controller
    {
        private CompanyDBContext DB;
        public WorksOnProjectController()
        {
            DB = new CompanyDBContext();
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddEmployeesToProjects(int id)
        {
            List<Project> projects = DB.Projects.Where(p => p.DeptNum == id).ToList();
            List<Employee> employees = DB.Employees.Where(p => p.deptId == id).ToList();

            ViewBag.emps = employees;

            return View(projects);
        }


        WorksOnProject? worksOnProject1;
        public IActionResult AddEmployeesToProjectsToDB(List<int> Projects, List<int> Employees)
        {
            

            foreach (var Project in Projects)
            {
                foreach (var employee in Employees)
                {
                    WorksOnProject worksOnProject = new WorksOnProject()
                    {
                        EmpSSN = employee,
                        projNum = Project
                    };
                    worksOnProject1 = DB.WorksOnProjects.Include(w => w.Project).SingleOrDefault(w => w.EmpSSN == worksOnProject.EmpSSN);
                    DB.WorksOnProjects.Add(worksOnProject);
                    DB.SaveChanges();
                }

            }

            ViewBag.emps = Employees;
            ViewBag.mgrSSN = (int)HttpContext.Session.GetInt32("SSN");

            return View(worksOnProject1);
        }
    }
}
