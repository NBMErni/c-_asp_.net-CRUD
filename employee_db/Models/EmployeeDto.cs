using System.ComponentModel.DataAnnotations;

namespace employee_db.Models
{
    public class EmployeeDto
    {
        [Required, MaxLength(30)]
        public string first_name { get; set; } = "";

        [Required, MaxLength(30)]
        public string last_name { get; set; } = "";

        [Required]
        public string gender { get; set; } = "";

        [Required]
        public string birth_date { get; set; } = "";

        [Required, MaxLength(30)]
        public string email { get; set; } = "";

        [Required]
        public string position { get; set; } = "";

     }
}
