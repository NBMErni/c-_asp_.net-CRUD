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

        [HttpGet("employees")]
        public IActionResult GetEmployees()
        {
            var employees = context.Employee.ToList();

            var employeesJson = System.Text.Json.JsonSerializer.Serialize(employees);
            System.Diagnostics.Debug.WriteLine("Employee DB TO >>>>> " + employeesJson);

            //return Content(employeesJson, "application/json");
            return Json(employees);
        }



        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(EmployeeDto EmployeeDto)
        {
            // validation 
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

            // passing the emp_data to DB and saving it
            context.Employee.Add(emp_data);
            context.SaveChanges();

            return RedirectToAction("Index", "Employee");
        }

        public IActionResult Edit(int id)
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


        [HttpPost]
        public IActionResult Edit(int id, EmployeeDto EmployeeDto)
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

        [HttpDelete("delete-employee/{id:int}")]
        public IActionResult Delete(int id)
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

    }
}