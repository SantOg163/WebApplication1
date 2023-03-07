using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace WebApplication1.Models
{
    public class CreateEmployee
    {
        public int? EmployeeId { get; set; }
        public string? Name { get; set; }
        public DateTime? DateOfBirth { get; set; }
        [ValidateNever]
        public string? Description { get; set; }
        public string? Department { get; set; }
        [ValidateNever]
        public string? Supervisor { get; set; }
        [ValidateNever]
        public bool? IsSupervisor { get; set; }
    }
}
