using System.Diagnostics;
using MongoDB.Driver;
using PTMKTest.Database;
using PTMKTest.Domain;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
var connectionString = configuration.GetConnectionString("DefaultConnection");

var services = new ServiceCollection();
services.AddSingleton(new MongoClient(connectionString).GetDatabase("PTMKTestDB"));
services.AddTransient<IEmployeeRepository, EmployeeRepository>();
services.AddTransient<EmployeeService>();

await using var serviceProvider = services.BuildServiceProvider();
var employeeService = serviceProvider.GetService<EmployeeService>();

if (employeeService is null)
    throw new NullReferenceException("employeeService is null");

Func<Task> action = args[0] switch
{
    "1" => () =>
    {
        Console.WriteLine("MongoDB не требует предварительного создания схемы данных. Начинайте работу!");
        return Task.CompletedTask;
    },
    "2" => async () =>
    {
        var employee = CreateEmployeeFrom(args);
        await employeeService.AddEmployee(employee);
        Console.WriteLine("Сотрудник добавлен");
    },
    "3" => async () =>
    {
        var employees = await employeeService.GetEmployees();

        foreach (var employee in employees)
            employee.Print();
    },
    "4" => async () =>
    {
        await employeeService.SeedRandomData();
        Console.WriteLine("База данных успешно заполнена");
    },
    "5" => async () =>
    {
        var timeList = new List<long>();
        var stopwatch = Stopwatch.StartNew();
        
        for (var index = 0; index < 100; index++)
        {
            stopwatch.Restart();
            await employeeService.GetFominsOnly();
            stopwatch.Stop();
            timeList.Add(stopwatch.ElapsedMilliseconds);
        }
        
        Console.WriteLine($"На поиск данных потребовалось: {timeList.Sum() / timeList.Count} ms");
    },
    _ => throw new ArgumentException("Такой режим работы отсутствует.")
};

await action.Invoke();
return;

Employee CreateEmployeeFrom(string[] args)
{
    var fullName = args[1];

    var year = int.Parse(args[2].Split('-')[0]);
    var month = int.Parse(args[2].Split('-')[1]);
    var day = int.Parse(args[2].Split('-')[2]);
    
    var birthday = new DateOnly(year, month, day);
    
    var isMale = args[3].ToUpper() == "MALE" || (args[3].ToUpper() == "FEMALE" ? false : throw new ArgumentException("Пол указан неверно"));

    return Employee.Create(fullName, birthday, isMale);
}