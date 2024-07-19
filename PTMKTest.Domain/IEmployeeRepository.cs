namespace PTMKTest.Domain;

public interface IEmployeeRepository
{
    Task AddEmployee(Employee newEmployee);
    Task AddEmployees(List<Employee> newEmployees);
    Task<List<Employee>> GetEmployees();
    Task<List<Employee>> GetFominsOnly();
}