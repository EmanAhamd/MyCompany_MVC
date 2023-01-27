using System.ComponentModel.DataAnnotations.Schema;

namespace MyCompany_MVC.Models
{
    public class Dependent
    {
        public string Name { get; set; }
        public string? Sex { get; set; }

        [Column(TypeName = "date")]
        public DateTime? BirthDate { get; set; }
        public string? Relationship { get; set; }

        [ForeignKey("Employee")]
        public int EmpSSN { get; set; }
        public virtual Employee Employee { get; set; }
    }
}
