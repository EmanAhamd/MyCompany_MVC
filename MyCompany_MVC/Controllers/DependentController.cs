using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyCompany_MVC.Models;

namespace MyCompany_MVC.Controllers
{
    public class DependentController : Controller
    {
        EmployeeController employeeController = new EmployeeController();
        private CompanyDBContext DB;
        public DependentController()
        {
            DB = new CompanyDBContext();

        }
        public IActionResult Index()
        {
            List<Dependent> dependents = DB.Dependents.ToList();
            return View("Index", dependents);
        }
        public IActionResult GetAllDependent()
        {

            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            List<Dependent> dependents = DB.Dependents.Where(d => d.EmpSSN == SSNfromSession).ToList();

            return View(dependents);
        }
        Int32 SSNfromSession;

        public IActionResult Add()
        {

            return View();
        }
        public IActionResult AddToDb(Dependent dependent)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            dependent.EmpSSN = SSNfromSession;
            DB.Dependents.Add(dependent);
            DB.SaveChanges();
            TempData["AddMsg"] = "You Add New Dependent";

            return RedirectToAction(nameof(GetAllDependent));

        }

        public IActionResult Edit(string id)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            Dependent dependent = DB.Dependents.SingleOrDefault(d => d.EmpSSN == SSNfromSession && d.Name == id);
            if (dependent == null)
                return View("Error");
            else
                return View(dependent);
        }
        public IActionResult EditToDb(Dependent dependentToEdit)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            Dependent olDdependent = DB.Dependents.SingleOrDefault(d => d.EmpSSN == SSNfromSession && d.Name == dependentToEdit.Name);
            olDdependent.Sex = dependentToEdit.Sex;
            olDdependent.Relationship = dependentToEdit.Relationship;
            olDdependent.BirthDate = dependentToEdit.BirthDate;

            DB.SaveChanges();
            return RedirectToAction(nameof(GetAllDependent));
        }

        public IActionResult Delete(string id)
        {
            SSNfromSession = (int)HttpContext.Session.GetInt32("SSN");
            Dependent? dependentToDelete = DB.Dependents.SingleOrDefault(d => d.EmpSSN == SSNfromSession && d.Name == id);

            DB.Dependents.Remove(dependentToDelete);
            DB.SaveChanges();
            return RedirectToAction(nameof(GetAllDependent));

        }

    }
}

   
