using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Department
    {
        [Key]
        public string? Dept_ID {  get; set; }

        [Required(ErrorMessage = "Inisial Departemen Tidak Boleh Kosong")]
        public string? Dept_Initial { get; set; }

        [Required(ErrorMessage = "Nama Departemen Tidak Boleh Kosong")]
        public string? Dept_Name { get; set; }
    }
}
