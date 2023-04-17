using Crud.WebApi.Data;
using Crud.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Crud.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly WebApiDbContext theWebApiDbContext;

        /// <summary>
        /// The controller is created and disposed on each request.
        /// </summary>
        /// <param name="aWebApiDbContext"></param>
        public EmployeesController(WebApiDbContext aWebApiDbContext)
        {
            this.theWebApiDbContext = aWebApiDbContext;
        }

        /// <summary>
        /// Gets all the employees from the database.
        /// </summary>
        /// <returns>Employees in JSON format.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await this.theWebApiDbContext.Employees.ToListAsync();
            return Ok(employees);
        }

        /// <summary>
        /// Gets an employee from the database based on the Guid provided.
        /// </summary>
        /// <returns>Employee in JSON format.</returns>
        [HttpGet]
        [Route("{theId}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid theId)
        {
            var anExistingEmployee = await this.theWebApiDbContext.Employees.FirstOrDefaultAsync(employee =>
                employee.Id == theId);
            if (anExistingEmployee == null)
            {
                return NotFound("Employee Not Found !!");
            }

            return Ok(anExistingEmployee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee theEmployee)
        {
            // Check for validations here.
            Regex emailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                RegexOptions.CultureInvariant | RegexOptions.Singleline);

            Regex phoneRegex = new Regex(@"^[0-9]{10}$");

            bool isValidDate = DateTime.TryParse(theEmployee.DateOfBirth.ToString(), out DateTime temp);

            if (theEmployee.Name.Length <= 0 ||
                !emailRegex.IsMatch(theEmployee.Email) ||
                !phoneRegex.IsMatch(theEmployee.Phone.ToString()) ||
                !isValidDate)
            {
                return BadRequest("Invalid data !!");
            }

            theEmployee.Id = Guid.NewGuid();
            await this.theWebApiDbContext.Employees.AddAsync(theEmployee);
            await this.theWebApiDbContext.SaveChangesAsync();

            return Ok(theEmployee);
        }

        [HttpPut]
        [Route("{theId}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid theId, [FromBody] Employee theEmployee)
        {
            theEmployee.Id = theId;

            var existingEmployee = await this.theWebApiDbContext.Employees.FirstOrDefaultAsync(employee =>
                employee.Id == theId);
            if (existingEmployee == null)
            {
                return NotFound("Employee Not Found !!");
            }

            existingEmployee.Name = theEmployee.Name;
            existingEmployee.Email = theEmployee.Email;
            existingEmployee.Phone = theEmployee.Phone;
            existingEmployee.DateOfBirth = theEmployee.DateOfBirth;

            await this.theWebApiDbContext.SaveChangesAsync();

            return Ok(existingEmployee);
        }

        [HttpDelete]
        [Route("{theId}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid theId)
        {
            var anExistingEmployee = await this.theWebApiDbContext.Employees.FirstOrDefaultAsync(employee =>
                employee.Id == theId);
            if (anExistingEmployee == null)
            {
                return NotFound("Employee Not Found !!");
            }

            this.theWebApiDbContext.Remove(anExistingEmployee);
            await this.theWebApiDbContext.SaveChangesAsync();

            return Ok(anExistingEmployee);
        }
    }
}
