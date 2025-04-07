using async_await.Data;
using async_await.Model;
using Microsoft.EntityFrameworkCore;

namespace async_await.LogicFunction;

public class StudentService
{
    public static void AddStudentManually(string? name)
    {
        var task = Task.Run(() =>
        {
            using var context = new AppDbContext();
            context.Students.Add(new Student { Name = name, IsAsyncAdded = false }); 
            context.SaveChanges();
            Console.WriteLine($"Добавлено в поток: {Environment.CurrentManagedThreadId}");
        });

        task.GetAwaiter().GetResult();
    }

    public static void ShowAllStudentsManually()
    {
        var task = Task.Run(() =>
        {
            using var context = new AppDbContext();
            var students = context.Students.Where(s => !s.IsAsyncAdded).ToList(); 
            Console.WriteLine($"Прочитано в поток: {Environment.CurrentManagedThreadId}");
            foreach (var student in students)
            {
                Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
            }
        });

        task.GetAwaiter().GetResult();
    }

    public static async Task AddStudentAsync(string? name)
    {
        await using var context = new AppDbContext();
        await context.Students.AddAsync(new Student { Name = name, IsAsyncAdded = true }); 
        await context.SaveChangesAsync();
        Console.WriteLine($"Добавлено асинхронно в поток: {Environment.CurrentManagedThreadId}");
    }

    public static async Task ShowAllStudentsAsync()
    {
        await using var context = new AppDbContext();
        var students = await context.Students.Where(s => s.IsAsyncAdded).ToListAsync(); 
        Console.WriteLine($"Прочитано асинхронно в поток: {Environment.CurrentManagedThreadId}");
        foreach (var student in students)
        {
            Console.WriteLine($"ID: {student.Id}, Name: {student.Name}");
        }
    }
}