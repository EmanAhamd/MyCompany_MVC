using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace MyCompany_MVC.Models
{
    public class Project
    {
        [Key]
        public int Number { get; set; }
        public string? Name { get; set; }
        public string? Location { get; set; }

        [ForeignKey("Department")]
        public int? DeptNum { get; set; }
        public virtual Department? Department { get; set; }
        public List<WorksOnProject>? WorksOnProjects { get; set; }
    }
}
