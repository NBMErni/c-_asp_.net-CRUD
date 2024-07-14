using employee_db.Services;
using Microsoft.AspNetCore.Mvc;

namespace employee_db.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext context;

        // Constructor for employee_info
        public EmployeeController(ApplicationDbContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            var employees = context.Employee.ToList();
            return View(employees);
        }
    }
}
