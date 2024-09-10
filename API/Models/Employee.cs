using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Employee
    {
        [Key]
        public string? Employee_Id {  get; set; }
        public string? FirstName { get; set; }
        public string? LastName {  get; set; }
        public string? Email {  get; set; }
        public virtual Department? Department { get; set; }
        [ForeignKey(nameof(Department))]
        public string? Dept_ID { get; set; }

        public virtual Account? Account { get; set; }
    }
}
