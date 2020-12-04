﻿using System.ComponentModel.DataAnnotations.Schema;

namespace EFCore_SQL
{
    [Table("Employee")]
    public class Employee
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
    }
}
