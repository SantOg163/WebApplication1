using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.TestPages
{
    public class EditPageModel : PageModel
    {
        public int _Id { get; set; }
        [BindProperty]
        public CreateEmployee CreateEmployee { get; set; }
        public Employees Employee { get; set; }
        public List<Departaments> Departaments { get; set; }
        public List<Employees> Employees { get; set; }
        private DataServices _services;
        public EditPageModel(DataServices services)
        {
            _services= services;
            CreateEmployee = new CreateEmployee();
            Departaments= _services.GetDepartaments().Result;
            Employees= _services.GetEmployees().Result;
            
        }
        public void OnGet(int id)
        {
            CreateEmployee= _services.GetCreateEmployee(id).Result;
        }
        public async Task<IActionResult> OnPost()
        {
            if (ModelState.IsValid)
            {
                CreateEmployee.EmployeeId= Convert.ToInt32($"{Request.QueryString}".Replace("?id=", "")); 
                List<string> errors = await _services.EditEmployee(CreateEmployee);
                if (errors.Count > 0)
                    foreach (string error in errors)
                        ModelState.AddModelError("", error);
                return RedirectToPage("/Index");
            }
            else
                return Page();
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            _services.DeleteEmployee(id);
            return RedirectToPage("/Index");
        }

    }
}
