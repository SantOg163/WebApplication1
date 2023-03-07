using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class Employees
    {
        [Key]
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string? Description { get; set; }
        public int DepartmentId { get; set; }
        public int? SupervisorId { get; set; }
        public bool IsSupervisor { get; set; } = false;
    }
}
