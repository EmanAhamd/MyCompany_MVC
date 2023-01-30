using Microsoft.AspNetCore.Mvc;

namespace MyCompany_MVC.Controllers
{
    public class CustomValidationController : Controller
    {
        public IActionResult ValidLocation(string Location)
        {
            if (Location.Contains("Cairo"))
            {
                return Json(true);
            }
            else if (Location.Contains("Giza"))
            {
                return Json(true);
            }
            else if (Location.Contains("Alex"))
            {
                return Json(true);
            }
            return Json(false);
        }
    }
}
