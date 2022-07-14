using System.ComponentModel.DataAnnotations;

namespace MVCClientApp.Models
{
    public class EmployeeDetail
    {
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }


        public int Age { get; set; }

  
        public string Phone { get; set; }
     
       
    }
}
