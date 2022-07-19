using System.ComponentModel.DataAnnotations;

namespace Backend_Api.Models
{
    public class Employees
    {
            
        [Key]
        public int Id { get; set; }


        public string Name { get; set; }


        public int Age { get; set; }


        public string Phone { get; set; }


    
}
}
