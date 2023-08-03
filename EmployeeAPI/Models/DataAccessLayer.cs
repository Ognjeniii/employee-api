using System.Data;
using System.Data.SqlClient;

namespace EmployeeAPI.Models
{
    public class DataAccessLayer
    {
        public Response GetAllEmployees(SqlConnection connection)
        {
            Response response = new Response();
            SqlDataAdapter adapter= new SqlDataAdapter("SELECT * FROM tblEmployee", connection);
            DataTable dataTable = new DataTable();
            List<Employee> lstEmployees = new List<Employee>();

            adapter.Fill(dataTable);
            if(dataTable.Rows.Count > 0 )
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    Employee employee = new Employee();

                    employee.Id = Convert.ToInt32(dataTable.Rows[i]["Id"]);
                    employee.Name = Convert.ToString(dataTable.Rows[i]["Name"]);
                    employee.Email = Convert.ToString(dataTable.Rows[i]["Email"]);
                    employee.IsActive = Convert.ToInt32(dataTable.Rows[i]["IsActive"]);

                    lstEmployees.Add(employee);
                }
            }
            if(lstEmployees.Count > 0 )
            {
                response.StatusCode = 200;
                response.StatusMessage = "Data found";
                response.ListEmployees = lstEmployees;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data found";
                response.ListEmployees = null;
            }

            return response;
        }

        public Response GetEmployeeById(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM tblEmployee WHERE ID = '" + id + "' AND IsActive = 1", connection);
            DataTable dataTable = new DataTable();
            Employee Employees = new Employee();

            adapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                    Employee employee = new Employee();

                    employee.Id = Convert.ToInt32(dataTable.Rows[0]["Id"]);
                    employee.Name = Convert.ToString(dataTable.Rows[0]["Name"]);
                    employee.Email = Convert.ToString(dataTable.Rows[0]["Email"]);
                    employee.IsActive = Convert.ToInt32(dataTable.Rows[0]["IsActive"]);

                    response.StatusCode = 200;
                    response.StatusMessage = "Data found";
                    response.Employee = employee;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data found";
                response.Employee = null;
            }

            return response;
        }

        public Response AddEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("INSERT INTO tblEmployee(Name, Email, IsActive, CreatedOn) VALUES ('"
                + employee.Name + "','" + employee.Email + "','" + employee.IsActive + "',GETDATE() )", connection);
            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee added.";
                response.Employee = employee;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data inserted.";
            }

            return response;
        }

        public Response UpdateEmployee(SqlConnection connection, Employee employee)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("UPDATE tblEmployee SET Name = '" + employee.Name + "'," +
                "Email =  '" + employee.Email + "' WHERE Id = '" + employee.Id + "' ", connection);
            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();

            if (i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee updated.";
                response.Employee = employee;
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No data updated.";
            }

            return response;
        }

        public Response DeleteEmployee(SqlConnection connection, int id)
        {
            Response response = new Response();
            SqlCommand sqlCommand = new SqlCommand("Delete from tblEmployee where id = '" + id + "' ", connection);
            connection.Open();
            int i = sqlCommand.ExecuteNonQuery();
            connection.Close();

            if(i > 0)
            {
                response.StatusCode = 200;
                response.StatusMessage = "Employee deleted";
            }
            else
            {
                response.StatusCode = 100;
                response.StatusMessage = "No employee deleted";
            }

            return response;
        }
    }
}
