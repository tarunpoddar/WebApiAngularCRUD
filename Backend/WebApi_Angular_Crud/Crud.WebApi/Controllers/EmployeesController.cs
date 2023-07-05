using AutoMapper;
using Crud.WebApi.Data;
using Crud.WebApi.DTOs;
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
        private readonly IMapper myMapper;

        /// <summary>
        /// The controller is created and disposed on each HTTP request.
        /// </summary>
        /// <param name="theWebApiDbContext">The database context will be injected by the framework.</param>
        public EmployeesController(WebApiDbContext theWebApiDbContext, IMapper theMapper)
        {
            this.myWebApiDbContext = theWebApiDbContext;
            this.myMapper = theMapper;
        }

        /// <summary>
        /// Gets all the employees from the database.
        /// </summary>
        /// <returns>Employees in JSON format.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var anEmployees = await this.myWebApiDbContext.Employees.ToListAsync();

            var result = this.myMapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeDTO>>(anEmployees);

            return Ok(result);
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

            var result = this.myMapper.Map<Employee, EmployeeDTO>(anExistingEmployee);

            return Ok(result);
        }

        /// <summary>
        /// Adds a new employee to the database on HTTPPost request.
        /// </summary>
        /// <param name="theEmployeeDTO">The employee object coming from the request body.</param>
        /// <returns>The newly created employee.</returns>
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] EmployeeDTO theEmployeeDTO)
        {
            // Check for validations.
            if (!ValidateEmployee(theEmployeeDTO))
            {
                return BadRequest("Invalid data !!");
            }

            // Assign the Id, any id passed by the user will be discarded.
            theEmployeeDTO.Id = Guid.NewGuid();

            // Map from the DTO to domain object.
            var anEmployee = this.myMapper.Map<EmployeeDTO, Employee>(theEmployeeDTO);

            // Add to database.
            await this.myWebApiDbContext.Employees.AddAsync(anEmployee);
            await this.myWebApiDbContext.SaveChangesAsync();

            return Ok(theEmployeeDTO);
        }

        /// <summary>
        /// Updates the employee on HTTPPut request.
        /// </summary>
        /// <param name="theId">The GUID of the employee to be updated from the URL route.</param>
        /// <param name="theEmployeeDTO">The employee object from the request body.</param>
        /// <returns>The updated employee object.</returns>
        [HttpPut]
        [Route("{theId}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid theId, [FromBody] EmployeeDTO theEmployeeDTO)
        {
            // Check for validations.
            if (!ValidateEmployee(theEmployeeDTO))
            {
                return BadRequest("Invalid data !!");
            }

            // Assign the existing ID and discard the Id coming from body.
            theEmployeeDTO.Id = theId;

            // Get the employee object which is in the database.
            var anExistingEmployee = await this.myWebApiDbContext.Employees.FirstOrDefaultAsync(employee =>
                employee.Id == theId);

            if (anExistingEmployee == null)
            {
                return NotFound("Employee Not Found !!");
            }

            // Map the exising objects.
            this.myMapper.Map(theEmployeeDTO, anExistingEmployee);

            await this.myWebApiDbContext.SaveChangesAsync();

            return Ok(theEmployeeDTO);
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

            var anEmployeeDTO = this.myMapper.Map<Employee, EmployeeDTO>(anExistingEmployee);

            return Ok(anEmployeeDTO);
        }

        /// <summary>
        /// Validates the incoming EmployeeDTO object.
        /// </summary>
        /// <returns>True is validation succeeds, else false.</returns>
        private bool ValidateEmployee(EmployeeDTO theEmployee)
        {
            // Check for validations here.
            Regex anEmailRegex = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                RegexOptions.CultureInvariant | RegexOptions.Singleline);

            Regex aPhoneRegex = new Regex(@"^[0-9]{10}$");

            bool aIsValidDate = DateTime.TryParse(theEmployee.EmployeeDateOfBirth.ToString(), out DateTime temp);

            if (string.IsNullOrEmpty(theEmployee.EmployeeName) ||
                 string.IsNullOrEmpty(theEmployee.EmployeeEmail) ||
                !anEmailRegex.IsMatch(theEmployee.EmployeeEmail) ||
                !aPhoneRegex.IsMatch(theEmployee.EmployeePhone.ToString()) ||
                !aIsValidDate)
            {
                return false;
            }

            return true;
        }
    }
}
