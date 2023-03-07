using Microsoft.EntityFrameworkCore;
using System.Text;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public class DataServices
    {
        private AppDbContext _context;
        public DataServices(AppDbContext context)
        {
            _context= context;
        }
        public async Task<List<Employees>> GetEmployees() => await _context.Employees.ToListAsync();
        public async Task<Employees> GetEmployee(int id) => _context.Employees.ToList().Where(e => e.EmployeeId==id).FirstOrDefault();
         
        public async Task<CreateEmployee> GetCreateEmployee(int id)
        {
            Employees employee = GetEmployee(id).Result;
            string Name = employee.Name;
            DateTime? DateOfBirth = employee.DateOfBirth;
            string Description = employee.Description;
            bool IsSupervisor = employee.IsSupervisor;
            string Supervisor;
            try
            {
                Supervisor=GetEmployees().Result.Where(e => e.EmployeeId==employee.SupervisorId).First().Name;
            }
            catch { Supervisor=""; }
            string Department = GetDepartaments().Result.Where(d => d.DepartamentId==employee.DepartmentId).FirstOrDefault().Name;
            int EmployeeId = employee.EmployeeId;
            return new CreateEmployee()
            {
                Name= Name,
                DateOfBirth=DateOfBirth,
                Description=Description,
                IsSupervisor= IsSupervisor,
                Supervisor =Supervisor,
                Department=Department,
                EmployeeId = EmployeeId
            };

        }
        public async Task<List<CreateEmployee>> GetCreateEmployees()
        {
            List<Employees> employees = GetEmployees().Result;
            List<CreateEmployee> createEmployees = new List<CreateEmployee>();
            foreach (Employees employee in employees)
            {
                string Name = employee.Name;
                DateTime? DateOfBirth = employee.DateOfBirth;
                string Description = employee.Description;
                bool IsSupervisor = employee.IsSupervisor;
                string? Supervisor;
                try
                {
                    Supervisor=employees.Where(e => e.EmployeeId==employee.SupervisorId).First().Name;
                }
                catch { Supervisor=""; }
                string Department = GetDepartaments().Result.Where(d => d.DepartamentId==employee.DepartmentId).FirstOrDefault().Name;
                int EmployeeId = employee.EmployeeId;
                createEmployees.Add(new CreateEmployee()
                {
                    Name= Name,
                    DateOfBirth=DateOfBirth,
                    Description=Description,
                    IsSupervisor= IsSupervisor,
                    Supervisor =Supervisor,
                    Department=Department,
                    EmployeeId = EmployeeId
                });
            }
            return createEmployees;
        }
        public async Task<List<Departaments>> GetDepartaments() => await _context.Departaments.ToListAsync();
        public async Task<List<string>> AddEmployee(CreateEmployee createEmployee)
        {
            List<string> errors = new List<string>();
            Employees employee = new Employees();
            int id;
            try
            {

                id= GetEmployees().Result.LastOrDefault().EmployeeId+1;
            }
            catch
            {
                id=1;
            }
            employee.EmployeeId=id;
            employee.Name= createEmployee.Name;
            employee.DateOfBirth= Convert.ToDateTime(createEmployee.DateOfBirth);
            employee.Description= createEmployee.Description;
            try
            {
                employee.DepartmentId= GetDepartaments().Result.Where(d => d.Name==createEmployee.Department).First().DepartamentId;
            }
            catch
            {
                errors.Add("Couldn't find the department");
            }
            try
            {
                if (createEmployee.Supervisor!=null)
                {
                    employee.SupervisorId= GetEmployees().Result.Where(e => e.Name==createEmployee.Supervisor).First().EmployeeId;
                    _context.Employees.Where(e => e.EmployeeId==employee.SupervisorId).First().IsSupervisor= true;
                }

            }
            catch
            {
                errors.Add("Couldn't find a supervisor");
            }
            if (errors.Count!=0)
                return errors;
            try
            {

                _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return errors;

        }
        public async Task<List<string>> EditEmployee(CreateEmployee createEmployee)
        {
            List<string> errors = new List<string>();
            Employees employee = _context.Employees.Where(e=>e.EmployeeId==createEmployee.EmployeeId).FirstOrDefault();
            employee.Name= createEmployee.Name;
            employee.DateOfBirth= Convert.ToDateTime(createEmployee.DateOfBirth);
            employee.Description= createEmployee.Description;
            try
            {
                employee.DepartmentId= GetDepartaments().Result.Where(d => d.Name==createEmployee.Department).First().DepartamentId;
            }
            catch
            {
                errors.Add("Couldn't find the department");
            }
            try
            {
                if (createEmployee.Supervisor!=null && employee.SupervisorId!=employee.EmployeeId)
                {
                    employee.SupervisorId= GetEmployees().Result.Where(e => e.Name==createEmployee.Supervisor).First().EmployeeId;
                    _context.Employees.Where(e => e.EmployeeId==employee.SupervisorId).First().IsSupervisor= true;
                }

            }
            catch
            {
                errors.Add("Couldn't find a supervisor");
            }
            if (errors.Count!=0)
                return errors;
            try
            {

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception();
            }
            return errors;

        }
        public async Task DeleteEmployee(int createEmployeeId)
        {
            try
            {
            _context.Employees.Remove(_context.Employees.Where(e=>e.EmployeeId==createEmployeeId).FirstOrDefault());
               await _context.SaveChangesAsync();
            }
            catch 
            {
                //на всякиий случай
            }
        }
    }
}

