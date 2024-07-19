using MongoDB.Bson;

namespace PTMKTest.Domain;

public class Employee
{
    public ObjectId Id { get; private set; }
    public string FullName { get; private init; } = string.Empty;
    public DateOnly Birthday { get; private init; }
    public bool IsMale { get; private init; }
    
    public int Age => DateTime.Now.Year - Birthday.Year;

    private Employee() { }

    public static Employee Create(string fullName, DateOnly birthday, bool isMale)
    {
        return new Employee
        {
            FullName = fullName.Trim(),
            Birthday = birthday,
            IsMale = isMale
        };
    }

    public void Print()
    {
        Console.Write("ФИО: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write($"{FullName, -30}");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" Дата рождения: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(Birthday);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" Пол: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(IsMale ? $"{"Male", -6}" : $"{"Female", -6}");
        Console.ForegroundColor = ConsoleColor.Black;
        Console.Write(" Полных лет: ");
        Console.ForegroundColor = ConsoleColor.Blue;
        Console.Write(Age);
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine();
    }
}