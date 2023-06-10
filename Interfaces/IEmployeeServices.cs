using SoftwareEngineerTest.Models;
using SoftwareEngineerTest.ViewModel;

namespace SoftwareEngineerTest.Interface
{
    public interface IEmployeeServices
    {
        public List<Employee> GetEmployees();
        public bool AddEmployee(EmployeeViewModel employee);
        public bool UpdateEmployee(Employee employee);
        public bool DeleteEmployee(int EmpId);
    }
}
