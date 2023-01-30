using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MyCompany_MVC.View_Model
{
    public class ProjectVM
    {
        public int Number { get; set; }
        [Display(Name = "Project Name")]
        [MinLength(5, ErrorMessage = "Name must be 5 charachter or  more than 5")]
        [Required(ErrorMessage = "name is required")]

        public string? Name { get; set; }
        [Required(ErrorMessage = "location is required")]
        [Display(Name = "project location")]
        [Remote("ValidLocation", "CustomValidation", AdditionalFields = "Location")]
        public string? Location { get; set; }
        [Compare("Location")]
        public string? ConfirmLocation { get; set; }
    }
}
