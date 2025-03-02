using System.ComponentModel.DataAnnotations;

namespace CodeFirst.Data.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string FirstName { get; set; }
    
    [Required]
    [MaxLength(20)]
    public string LastName { get; set; }
    
    [Required]
    public string Email { get; set; }
    
    [Required]
    [MaxLength(16)]
    public string PhoneNumber { get; set; }

    public Customer(string firstName, string lastName, string email, string phoneNumber)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
    }
}

