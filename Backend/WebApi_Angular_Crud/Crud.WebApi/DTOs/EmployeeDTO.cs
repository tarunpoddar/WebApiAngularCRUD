namespace Crud.WebApi.DTOs
{
    public class EmployeeDTO
    {
        public Guid Id { get; set; }
        public string EmployeeName { get; set; }
        public string EmployeeEmail { get; set; }
        public long EmployeePhone { get; set; } 
        public DateTime EmployeeDateOfBirth { get; set; }
    }
}
