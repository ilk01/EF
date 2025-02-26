using Dapper;
using Microsoft.Data.SqlClient;
namespace ConsoleApp8;

public class CarService
{
    private readonly string? _connectionString;

    public CarService(string? connectionString)
    {
        _connectionString = connectionString;
    }

    public void AddCar(Car car)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var sqlQuery = "INSERT INTO Cars (Brand, Model, Year, Price) VALUES (@Brand, @Model, @Year, @Price)";

            connection.Execute(sqlQuery, new { car.Brand, car.Model, car.Year, car.Price });
        }
    }



    public void UpdateCarPrice(int id, decimal newPrice)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var sqlQuery = "UPDATE Cars SET Price = @Price WHERE Id = @Id";
            connection.Execute(sqlQuery, new { Price = newPrice, Id = id });
        }
    }
    
    
    public void DeleteCar(int id)
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var sqlQuery = "DELETE FROM Cars WHERE Id = @Id";
            connection.Execute(sqlQuery, new { Id = id });
        }
    }
    
    
    public void GetAllCars()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            var sqlQuery = "SELECT * FROM Cars";
            var cars = connection.Query<Car>(sqlQuery).ToList();
            foreach (var Car in cars)
            {
                Console.WriteLine($"Brand = {Car.Brand} Model = {Car.Model} Year = {Car.Year} Price = {Car.Price}");
            }
        }
    }

    string sqlQuery = "SELECT * FROM Cars";
}