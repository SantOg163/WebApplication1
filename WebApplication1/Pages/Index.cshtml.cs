using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApplication1.Data;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        public List<CreateEmployee> Employees { get; set; }
        private DataServices _dataServices;
        public IndexModel(DataServices dataServices)
        { 
            _dataServices = dataServices;
            Employees= dataServices.GetCreateEmployees().Result;
            
        }

        public async void OnGet()
        {
        }
        public async void OnPost()
        {
        }
    }
}