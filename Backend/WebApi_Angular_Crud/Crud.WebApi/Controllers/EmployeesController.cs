using Crud.WebApi.Data;
using Crud.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace Crud.WebApi.Controllers
{
    /// <summary>
    /// API contoller for manupulating employee objects.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly WebApiDbContext myWebApiDbContext;

        /// <summary>
        /// The controller is created and disposed on each HTTP request.
        /// </summary>
        /// <param name="theWebApiDbContext">The database context will be injected by the framework.</param>
        public EmployeesController(WebApiDbContext theWebApiDbContext)
        {
            this.myWebApiDbContext = theWebApiDbContext;
        }

        /// <summary>
        /// Gets all the employees from the database.
        /// </summary>
        /// <returns>Employees in JSON format.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var anEmployees = await this.myWebApiDbContext.Employees.ToListAsync();
            return Ok(anEmployees);
        }

        /// <summary>
        /// Gets an employee from the database based on the Guid provided.
        /// </summary>
        /// <returns>Employee in JSON format.</returns>
        [HttpGet]
        [Route("{theId}")]
        public async Task<IActionResult> GetEmployee([FromRoute] Guid theId)
        {
            var anExistingEmployee = await this.myWebApiDbContext.Employees.FirstOrDefaultAsync(employee =>
                employee.Id == theId);
            
            if (anExistingEmployee == null)
            {
                return NotFound("Employee Not Found !!");
            }

            return Ok(anExistingEmployee);
        }

        /// <summary>
        /// Adds a new employee to the database on HTTPPost request.
        /// </summary>
        /// <param name="theEmployee">The employee object coming from the request body.</param>
        /// <returns>The newly created employee.</returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee theEmployee)
        {
            // Check for validations here.
            Regex anEmailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                RegexOptions.CultureInvariant | RegexOptions.Singleline);

            Regex aPhoneRegex = new Regex(@"^[0-9]{10}$");

            bool aIsValidDate = DateTime.TryParse(theEmployee.DateOfBirth.ToString(), out DateTime temp);

            if (theEmployee.Name.Length <= 0 ||
                !anEmailRegex.IsMatch(theEmployee.Email) ||
                !aPhoneRegex.IsMatch(theEmployee.Phone.ToString()) ||
                !aIsValidDate)
            {
                return BadRequest("Invalid data !!");
            }

            theEmployee.Id = Guid.NewGuid();
            await this.myWebApiDbContext.Employees.AddAsync(theEmployee);
            await this.myWebApiDbContext.SaveChangesAsync();

            return Ok(theEmployee);
        }

        /// <summary>
        /// Updates the employee on HTTPPut request.
        /// </summary>
        /// <param name="theId">The GUID of the employee to be updated from the URL route.</param>
        /// <param name="theEmployee">The employee object from the request body.</param>
        /// <returns>The updated employee object.</returns>
        [HttpPut]
        [Route("{theId}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid theId, [FromBody] Employee theEmployee)
        {
            theEmployee.Id = theId;

            var anExistingEmployee = await this.myWebApiDbContext.Employees.FirstOrDefaultAsync(employee =>
                employee.Id == theId);

            if (anExistingEmployee == null)
            {
                return NotFound("Employee Not Found !!");
            }

            anExistingEmployee.Name = theEmployee.Name;
            anExistingEmployee.Email = theEmployee.Email;
            anExistingEmployee.Phone = theEmployee.Phone;
            anExistingEmployee.DateOfBirth = theEmployee.DateOfBirth;

            await this.myWebApiDbContext.SaveChangesAsync();

            return Ok(anExistingEmployee);
        }

        /// <summary>
        /// Deletes an employee from the database.
        /// </summary>
        /// <param name="theId">The GUID of the employee to be deleted.</param>
        /// <returns>The deleted employee object.</returns>
        [HttpDelete]
        [Route("{theId}")]
        public async Task<IActionResult> DeleteEmployee([FromRoute] Guid theId)
        {
            var anExistingEmployee = await this.myWebApiDbContext.Employees.FirstOrDefaultAsync(employee =>
                employee.Id == theId);
            
            if (anExistingEmployee == null)
            {
                return NotFound("Employee Not Found !!");
            }

            this.myWebApiDbContext.Remove(anExistingEmployee);
            await this.myWebApiDbContext.SaveChangesAsync();

            return Ok(anExistingEmployee);
        }
    }
}
