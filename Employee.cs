using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EmployeeDeptWebAPIDemo.Models
{
    public class Employee
    {
        public int EmpId { get; set; }
        public string EmpName { get; set; }
        public decimal Salary { get; set; }
        public int DeptId { get; set; }
        public string DeptName { get; set; }   // for JOIN result
    }
}