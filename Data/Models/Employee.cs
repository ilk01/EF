using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Data.Models;



public class Employee
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    public Positions Position { get; set; }

    public Employee(string firstName, string lastName, Positions position)
    {
        FirstName = firstName;
        LastName = lastName;
        Position = position;
    }
    
    public enum Positions
    {
        Manager,
        Salesperson,
        Technician,
        Accountant
    }
}


