namespace PTMKTest.Domain;

public class EmployeeService(IEmployeeRepository repository)
{
    public async Task AddEmployee(Employee newEmployee)
    {
        await repository.AddEmployee(newEmployee);
    }

    public async Task<List<Employee>> GetEmployees()
    {
        return await repository.GetEmployees();
    }
    
    public async Task SeedRandomData()
    {
        var maleFirstNames = new List<string>()
        {
            "Alex", "Maxim", "Oleg", "Dmitry", "Nick", "Ivan", "Eugene", "Leo", "Sergey"
        };

        var femaleFirstNames = new List<string>()
        {
            "Anna", "Maria", "Olya", "Daria", "Nina", "Ioanna", "Eva", "Lena", "Svetlana"
        };

        var maleSecondNames = new List<string>()
        {
            "Arhipov", "Ivanov", "Nekrasov", "Berezin", "Denisov", "Kozlov", "Orlov"
        };
        
        var femaleSecondNames = new List<string>()
        {
            "Arhipova", "Ivanova", "Nekrasova", "Berezina", "Denisova", "Kozlova", "Orlova"
        };

        var maleSurnames = new List<string>()
        {
            "Andreevich", "Maximovich", "Olegovich", "Dmitrievich", "Nickolaevich", "Ivanovich", "Leonidovich", "Sergeevich"
        };
        
        var femaleSurnames = new List<string>()
        {
            "Andreevna", "Maximova", "Olegovna", "Dmitrievna", "Nickolaevna", "Ivanovna", "Leonidovna", "Sergeevna"
        };

        var employees = new List<Employee>();
        
        for (var index = 0; index < 1_000_000; index++)
        {
            var isMale = Random.Shared.Next(0, 2) == 0;

            var surname = isMale ? maleSurnames[Random.Shared.Next(0, 8)] : femaleSurnames[Random.Shared.Next(0, 8)];
            var firstName = isMale ? maleFirstNames[Random.Shared.Next(0, 9)] : femaleFirstNames[Random.Shared.Next(0, 9)];
            var secondName = isMale ? maleSecondNames[Random.Shared.Next(0, 7)] : femaleSecondNames[Random.Shared.Next(0, 7)];

            var employee = Employee.Create(
                fullName: $"{surname} {firstName} {secondName}",
                birthday: new DateOnly(Random.Shared.Next(1980, 2014), Random.Shared.Next(3, 13), Random.Shared.Next(1, 30)), 
                isMale: isMale);
            
            employees.Add(employee);
        }
        
        for (var index = 0; index < 100; index++)
        {
            var surname = "Fomin";
            var firstName = maleFirstNames[Random.Shared.Next(0, 9)];
            var secondName = maleSecondNames[Random.Shared.Next(0, 7)];

            var employee = Employee.Create(
                fullName: $"{surname} {firstName} {secondName}",
                birthday: new DateOnly(Random.Shared.Next(1980, 2014), Random.Shared.Next(3, 13), Random.Shared.Next(1, 30)), 
                isMale: true);
            
            employees.Add(employee);
        }
        
        await repository.AddEmployees(employees);
    }
    
    public async Task<List<Employee>> GetFominsOnly()
    {
        return await repository.GetFominsOnly();
    }
}