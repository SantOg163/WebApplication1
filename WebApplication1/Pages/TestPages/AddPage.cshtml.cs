using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages.TestPages
{
    public class AddPageModel : PageModel
    {
        [BindProperty]
        public CreateEmployee CreateEmployee { get; set; }
        public Employees Employee { get; set; }
        public List<Departaments> Departaments { get; set; }
        public List<Employees> Employees { get; set; }
        private DataServices _services;
        public AddPageModel(DataServices services) 
        {
            _services= services;
            CreateEmployee = new CreateEmployee();
            Departaments= _services.GetDepartaments().Result;
            Employees= _services.GetEmployees().Result;
        }
        public void OnGet()
        {
        }
        public async Task<IActionResult> OnPost() 
        {
            if (ModelState.IsValid)
            {
                List<string> errors = await _services.AddEmployee(CreateEmployee);
                if (errors.Count > 0)
                {
                    foreach (string error in errors)
                        ModelState.AddModelError("", error);
                    return Page();
                }
                return RedirectToPage("/Index");
            }
            else 
                return Page();
        }

    }
}
