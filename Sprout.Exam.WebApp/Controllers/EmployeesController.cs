using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Sprout.Exam.Business.DataTransferObjects;
using Sprout.Exam.Common.Enums;
using Sprout.Exam.WebApp.Models;
using Sprout.Exam.WebApp.Data;
using Microsoft.EntityFrameworkCore;

namespace Sprout.Exam.WebApp.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly EmployeeContext _context;

        public EmployeesController(EmployeeContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var employees = await _context.WorkEmployees.ToListAsync();

            return Ok(employees);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and fetch from the DB.
        /// </summary>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.WorkEmployees.FindAsync(id);

            return Ok(employee);
        }

        /// <summary>
        /// Refactor this method to go through proper layers and update changes to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, EditEmployeeDto input)
        {
            if (id != input.Id)
            {
                return BadRequest();
            }

            var employee = await _context.WorkEmployees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.FullName = input.FullName;
            employee.Birthdate = input.Birthdate.ToString("yyyy-MM-dd");
            employee.Tin = input.Tin;
            employee.TypeId = input.TypeId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!EmployeeExists(id))
            {
                return NotFound();
            }

            return NoContent();
        }

        /// <summary>
        /// Refactor this method to go through proper layers and insert employees to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Post(CreateEmployeeDto input)
        {
            if (EmployeeExists(input.Tin))
            {
                return BadRequest($"Another user has already the same TIN id: {input.Tin}");
            }

            var employee = new WorkEmployee
            {
                FullName = input.FullName,
                Tin = input.Tin,
                Birthdate = input.Birthdate.ToString("yyyy-MM-dd"),
                TypeId = input.TypeId
            };

            _context.WorkEmployees.Add(employee);
            await _context.SaveChangesAsync();

            return Created($"/api/employees/{employee.Id}", employee.Id);
        }


        /// <summary>
        /// Refactor this method to go through proper layers and perform soft deletion of an employee to the DB.
        /// </summary>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _context.WorkEmployees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            _context.WorkEmployees.Remove(employee);
            await _context.SaveChangesAsync();

            return Ok(id);
        }


        /// <summary>
        /// Calculate the net income of the selected employee
        /// </summary>
        /// <param name="id"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        [HttpPost("{id}/calculate")]
        public async Task<IActionResult> Calculate(int id, [FromBody] CalculateSalaryParamsDto param)
        {
            var employeeData = await _context.WorkEmployees.FindAsync(id);

            if (employeeData == null)
            {
                return NotFound();
            }

            // Throw a BadRequest if absent days are outside the expected range
            if (param.absentDays < 0 || param.absentDays > Salary.MONTH_WORK_DAYS)
            {
                return BadRequest($"absentDays must be >= 0 and <= {Salary.MONTH_WORK_DAYS}");
            }

            var type = (EmployeeType)employeeData.TypeId;
            var employee = EmployeeFactory.GetEmployee(type);

            return type switch
            {
                EmployeeType.Regular =>
                    //create computation for regular.
                    Ok(employee.GetSalary(param.absentDays)),
                EmployeeType.Contractual =>
                    //create computation for contractual.
                    Ok(employee.GetSalary(param.workedDays)),
                _ => NotFound("Employee Type not found")
            };
        }

        private bool EmployeeExists(long id) => _context.WorkEmployees.Any(m => m.Id == id);
        private bool EmployeeExists(string tin) => _context.WorkEmployees.Any(m => m.Tin == tin);
    }
}
