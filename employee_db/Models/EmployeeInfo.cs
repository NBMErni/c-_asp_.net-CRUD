using System.ComponentModel.DataAnnotations;

namespace employee_db.Models
{
    public class EmployeeInfo
    {
        public int Id { get; set; }

     
        public string first_name { get; set; } = "";

       
        public string last_name { get; set; } = "";

    
        public string gender { get; set; } = "";


        public string birth_date { get; set; } = "";

        public string email { get; set; } = "";

     
        public string position { get; set; } = "";

    }

}
