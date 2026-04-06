using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Optimization;
using System.Configuration;
using System.Data.SqlClient;
using EmployeeDeptWebAPIDemo.Models;
using System.Web.Http.Cors;
namespace EmployeeDeptWebAPIDemo.Controllers
{
  
    public class EmployeeController : ApiController
    {
        string cs = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;
        [HttpGet]
        public List<Employee> GetEmployees()
        {
            List<Employee> list = new List<Employee>();

            string cs = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"SELECT e.EmpId,e.EmpName,e.Salary,e.DeptId,d.DeptName
                         FROM Employee e
                         INNER JOIN Department d
                         ON e.DeptId = d.DeptId";

                SqlCommand cmd = new SqlCommand(query, con);

                con.Open();

                SqlDataReader dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    Employee emp = new Employee();

                    emp.EmpId = Convert.ToInt32(dr["EmpId"]);
                    emp.EmpName = dr["EmpName"].ToString();
                    emp.Salary = Convert.ToDecimal(dr["Salary"]);
                    emp.DeptId = Convert.ToInt32(dr["DeptId"]);
                    emp.DeptName = dr["DeptName"].ToString();

                    list.Add(emp);
                }
            }

            return list;
        }
        [HttpPost]
        public string InsertEmployee(Employee emp)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "insert into Employee(EmpName,Salary,DeptId) values(@name,@salary,@dept)";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@name", emp.EmpName);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@dept", emp.DeptId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return "Employee Added";
        }
        [HttpPut]
        public string UpdateEmployee(Employee emp)
        {
            string cs = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = @"UPDATE Employee 
                         SET EmpName=@name,
                             Salary=@salary,
                             DeptId=@deptid
                         WHERE EmpId=@id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", emp.EmpId);
                cmd.Parameters.AddWithValue("@name", emp.EmpName);
                cmd.Parameters.AddWithValue("@salary", emp.Salary);
                cmd.Parameters.AddWithValue("@deptid", emp.DeptId);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return "Employee Updated Successfully";
        }
        [HttpDelete]
        public string DeleteEmployee(int id)
        {
            string cs = ConfigurationManager.ConnectionStrings["conStr"].ConnectionString;

            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "DELETE FROM Employee WHERE EmpId=@id";

                SqlCommand cmd = new SqlCommand(query, con);

                cmd.Parameters.AddWithValue("@id", id);

                con.Open();
                cmd.ExecuteNonQuery();
            }

            return "Employee Deleted Successfully";
        }
    }
}
