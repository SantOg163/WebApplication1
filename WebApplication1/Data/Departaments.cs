using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Data
{
    public class Departaments
    {
        [Key]
        public int DepartamentId { get; set; }
        public string Name { get;set; }
    }
}
