using System;
using System.Collections.Generic;

namespace MVCPortfolio.Models
{
    public partial class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int? Age { get; set; }
        public string? Phone { get; set; }

        public string Location { get; set; }
    }
}
