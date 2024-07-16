using employee_db.Models;
using employee_db.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

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
            return View();
        }

        // Get all employees from DB and convert it into Json
        [HttpGet("employees")] // 
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = context.Employee.ToList();
                return Json(employees);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        // View for create page
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto EmployeeDto)
        {
            try
            {
                // Validation 
                if (!ModelState.IsValid)
                {
                    return View(EmployeeDto);
                }

                // Creating data obj to save in DB
                EmployeeInfo emp_data = new EmployeeInfo()
                {
                    first_name = EmployeeDto.first_name,
                    last_name = EmployeeDto.last_name,
                    gender = EmployeeDto.gender,
                    birth_date = EmployeeDto.birth_date,
                    email = EmployeeDto.email,
                    position = EmployeeDto.position,
                };

                // Passing the emp_data to DB and saving it
                context.Employee.Add(emp_data);
                context.SaveChanges();

                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        public IActionResult Edit(int id)
        {
            try
            {
                var employee = context.Employee.Find(id);

                if (employee == null)
                {
                    return RedirectToAction("Index", "Employee");
                }

                var employeeDto = new EmployeeDto()
                {
                    first_name = employee.first_name,
                    last_name = employee.last_name,
                    gender = employee.gender,
                    birth_date = employee.birth_date,
                    email = employee.email,
                    position = employee.position
                };

                ViewData["employee_id"] = employee.Id;

                return View(employeeDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpPost]
        public IActionResult Edit(int id, EmployeeDto EmployeeDto)
        {
            try
            {
                var employee = context.Employee.Find(id);

                if (employee == null)
                {
                    return RedirectToAction("Index", "Employee");
                }

                if (!ModelState.IsValid)
                {
                    ViewData["employee_id"] = employee.Id;
                    return View(EmployeeDto);
                }

                // Update product details
                employee.first_name = EmployeeDto.first_name;
                employee.last_name = EmployeeDto.last_name;
                employee.gender = EmployeeDto.gender;
                employee.birth_date = EmployeeDto.birth_date;
                employee.email = EmployeeDto.email;
                employee.position = EmployeeDto.position;

                context.SaveChanges();

                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }

        [HttpDelete("delete-employee/{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var employee = context.Employee.Find(id);
                if (employee == null)
                {
                    return RedirectToAction("Index", "Employee");
                }

                context.Employee.Remove(employee);
                context.SaveChanges(true);

                return RedirectToAction("Index", "Employee");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(500, "Internal server error");
            }
        }


    }
}