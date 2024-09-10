using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace API.Models
{
    public class Account
    {
        [Key]
        [ForeignKey("Employee")]
        public string? Account_ID {  get; set; }
        public string? Username {  get; set; }
        public string? Password { get; set; }


        public virtual Employee? Employee { get; set; }
    }
}
