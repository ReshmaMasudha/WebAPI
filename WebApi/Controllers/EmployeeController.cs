using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient.Memcached;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/Employee")]
    [ApiController]

    public class EmployeeController : ControllerBase
    {
        [HttpGet("GetEmployeeById/{id}")]                  //applying the verb attribute for the web api to match with the request method and endpoint
        public Employee GetEmployee(int id)
        {
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-4ENFHGG\MSSQLSERVER01;Database=database1;User ID=sa;Password=1234;";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from dbo.Company where EmployeeId=" + id + "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;
            while (reader.Read())
            {
                emp = new Employee();
                emp.EmployeeId = Convert.ToInt32(reader.GetValue(0));
                emp.ManagerId = Convert.ToInt32(reader.GetValue(1));
                emp.Name = reader.GetValue(2).ToString();
            }
            return emp;
            myConnection.Close();
        }

       

        [HttpGet("GetAllEmployees")]                  //applying the verb attribute for the web api to match with the request method and endpoint
        public List<Employee> GetAllEmployees()
        {
            List<Employee> result = new List<Employee>();
            SqlDataReader reader = null;
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-4ENFHGG\MSSQLSERVER01;Database=database1;User ID=sa;Password=1234;";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "Select * from dbo.Company ";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            reader = sqlCmd.ExecuteReader();
            Employee emp = null;

            if(reader.HasRows)
            {
                while (reader.Read())
                {
                    emp = new Employee();
                    emp.EmployeeId = Convert.ToInt32(reader.GetValue(0));
                    emp.ManagerId = Convert.ToInt32(reader.GetValue(1));
                    emp.Name = reader.GetValue(2).ToString();

                    result.Add(emp);
                }
            }
           
            return result;
            myConnection.Close();
        }
        // POST: api/Employee
        [HttpPost("AddEmployee")]
       // [ServiceFilter(typeof(ValidationFilterAttribute))]
        public void AddEmployee(Employee employee)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-4ENFHGG\MSSQLSERVER01;Database=database1;User ID=sa;Password=1234;";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "INSERT INTO dbo.Company (EmployeeId,ManagerId,EmployeeName) Values (@EmployeeId,@ManagerId,@Name)";
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            sqlCmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId);        //input's values are passed in parameters
            sqlCmd.Parameters.AddWithValue("@Name", employee.Name);
           
            myConnection.Open();    
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        [HttpPut("UpdateEmployee")]
        public void UpdateEmployee(Employee employee)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-4ENFHGG\MSSQLSERVER01;Database=database1;User ID=sa;Password=1234;";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "UPDATE dbo.Company SET ManagerId = @ManagerId ,EmployeeName = @Name Where EmployeeId = @EmployeeId";
            sqlCmd.Connection = myConnection;
            sqlCmd.Parameters.AddWithValue("@EmployeeId", employee.EmployeeId);
            sqlCmd.Parameters.AddWithValue("@ManagerId", employee.ManagerId);        //input's values are passed in parameters
            sqlCmd.Parameters.AddWithValue("@Name", employee.Name);
            myConnection.Open();
            int rowInserted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("DeleteEmployeeById/{id}")]
        public void DeleteEmployeeByID(int id)
        {
            SqlConnection myConnection = new SqlConnection();
            myConnection.ConnectionString = @"Server=DESKTOP-4ENFHGG\MSSQLSERVER01;Database=database1;User ID=sa;Password=1234;";
            SqlCommand sqlCmd = new SqlCommand();
            sqlCmd.CommandType = CommandType.Text;
            sqlCmd.CommandText = "delete from dbo.Company where EmployeeId = " + id+ "";
            sqlCmd.Connection = myConnection;
            myConnection.Open();
            int rowDeleted = sqlCmd.ExecuteNonQuery();
            myConnection.Close();
        }
    }
}
