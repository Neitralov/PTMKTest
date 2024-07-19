using MongoDB.Bson;
using MongoDB.Driver;
using PTMKTest.Domain;

namespace PTMKTest.Database;

public class EmployeeRepository(IMongoDatabase database) : IEmployeeRepository
{
    public async Task AddEmployee(Employee newEmployee)
    {
        var collection = database.GetCollection<Employee>(nameof(Employee));
        
        await collection.InsertOneAsync(newEmployee);
    }

    public async Task AddEmployees(List<Employee> newEmployees)
    {
        var collection = database.GetCollection<Employee>(nameof(Employee));

        await collection.InsertManyAsync(newEmployees);
    }

    public async Task<List<Employee>> GetEmployees()
    {
        var collection = database.GetCollection<Employee>(nameof(Employee));
        
        var employees = await collection.Find("{ }").Sort("{ FullName: 1 }").ToListAsync();
        return employees.DistinctBy(x => x.FullName).DistinctBy(x => x.Birthday).ToList();
    }

    public async Task<List<Employee>> GetFominsOnly()
    {
        var collection = database.GetCollection<Employee>(nameof(Employee));

        return await collection.Find("{ FullName: /^F/i }").ToListAsync();
    }
}