using SoftwareEngineerTest.Controllers;
using SoftwareEngineerTest.Interface;
using SoftwareEngineerTest.Models;
using SoftwareEngineerTest.ViewModel;
using System.Data;
using System.Data.SqlClient;

namespace SoftwareEngineerTest.Services
{
    public class EmployeeService : IEmployeeServices
    {
        // ConnectionString is based on the database
        public string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;Initial Catalog=SoftwareEngineerTest;Integrated Security=True";
        public SqlConnection con;
        public EmployeeService()
        {
            
        }
        public bool AddEmployee(EmployeeViewModel employee)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    string storedProcedure = "CreateEmployee";
                    /*
                     CREATE PROCEDURE CreateEmployee
                        @Names NVARCHAR(100),
                        @DOB DATE,
                        @Salary DECIMAL(18, 2),
                        @YearOfExp INT
                    AS
                    BEGIN
                        INSERT INTO Employee (Names, DOB, Salary, YearOfExp)
                        VALUES (@Names, @DOB, @Salary, @YearOfExp);
                    END

                     */
                    con.Open();
                    var cmd = new SqlCommand(storedProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    // Add parameters
                    cmd.Parameters.AddWithValue("@Names", employee.Names);
                    cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@YearOfExp", employee.YearOfExp);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    // If any rows were affected, the create was successful
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool DeleteEmployee(int empId)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    string storedProcedure = "DeleteEmployee";

                    /*
                    Create the procedure like this at database

                    CREATE PROCEDURE DeleteEmployee
                        @EmpId INT
                    AS
                    BEGIN
                        DELETE FROM Employee
                        WHERE EmpId = @EmpId;
                    END


                    */

                    con.Open();
                    var cmd = new SqlCommand(storedProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpId", empId);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    // If any rows were affected, the update was successful
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();

            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    string storedProcedure = "GetAllEmployees";
                    /*
                    Create the procedure like this at database

                    CREATE PROCEDURE GetAllEmployees
                    AS
                    BEGIN
                        SELECT *
                        FROM Employee
                        ORDER BY EmpId;
                    END


                    */
                    con.Open();
                    var cmd = new SqlCommand(storedProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlDataReader rdr = cmd.ExecuteReader();

                    while (rdr.Read())
                    {
                        Employee emp = new Employee();
                        emp.EmpId = Convert.ToInt32(rdr["EmpId"]);
                        emp.Names = Convert.ToString(rdr["Names"]);
                        emp.DOB = Convert.ToDateTime(rdr["DOB"]);
                        emp.Salary = Convert.ToDouble(rdr["Salary"]);
                        emp.YearOfExp = Convert.ToInt32(rdr["YearOfExp"]);

                        employees.Add(emp);
                    }
                    con.Close();
                }
                return employees;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateEmployee(Employee employee)
        {
            try
            {
                using (con = new SqlConnection(connectionString))
                {
                    string storedProcedure = "UpdateEmployee";
                    /*
                    Create the procedure like this at database

                    CREATE PROCEDURE UpdateEmployee
                        @EmpId INT,
                        @Names VARCHAR(100),
                        @DOB DATE,
                        @Salary FLOAT,
                        @YearOfExp INT
                    AS
                    BEGIN
                        UPDATE Employee
                        SET Names = @Names,
                            DOB = @DOB,
                            Salary = @Salary,
                            YearOfExp = @YearOfExp
                        WHERE EmpId = @EmpId;
                    END
                    */

                    con.Open();
                    var cmd = new SqlCommand(storedProcedure, con);
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@EmpId", employee.EmpId);
                    cmd.Parameters.AddWithValue("@Names", employee.Names);
                    cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                    cmd.Parameters.AddWithValue("@Salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@YearOfExp", employee.YearOfExp);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    con.Close();

                    // If any rows were affected, the update was successful
                    return rowsAffected > 0;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
