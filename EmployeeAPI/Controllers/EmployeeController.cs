using EmployeeAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace EmployeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IConfiguration _configuration;
       
        public EmployeeController(IConfiguration configuration)
        {
            _configuration = configuration; 
        }


        [HttpGet]
        [Route("GetAllEmployees")]
        public Response GetAllEmployees()
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection").ToString());
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            response = dal.GetAllEmployees(sqlConnection);
            return response;
        }

        [HttpGet]
        [Route("GetEmployeeById/{id}")]
        public Response GetEmployeeById(int id)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection").ToString());
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            response = dal.GetEmployeeById(sqlConnection, id);
            return response;
        }

        [HttpPost]
        [Route("AddEmployee")]
        public Response AddEmployee(Employee employee)
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection").ToString());
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            response = dal.AddEmployee(sqlConnection, employee);

            return response;
        }

        [HttpPut]
        [Route("UpdateEmployee")]
        public Response UpdateEmployee(Employee employee) 
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection").ToString());
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();    
            response = dal.UpdateEmployee(sqlConnection, employee);

            return response;
        }

        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public Response DeleteEmployee(int id) 
        {
            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("EmployeeConnection").ToString());
            Response response = new Response();
            DataAccessLayer dal = new DataAccessLayer();
            response = dal.DeleteEmployee(sqlConnection, id);

            return response;
        }
    }
}
