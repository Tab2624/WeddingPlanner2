#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Add this using statement to access NotMapped
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;

namespace WeddingPlanner2.Models;
public class Wedding
{
    [Key]
    public int WeddingId {get; set;}

    [Required(ErrorMessage = "Field is required")]
    public string Wedder1 {get; set;}
    [Required(ErrorMessage = "Field is required")]
    public string Wedder2 {get; set;}

    [DateMustBeInFuture]
    [Required(ErrorMessage = "Field is required")]
    [DataType(DataType.Date)]
    public DateTime Date {get; set;}

    [Required(ErrorMessage = "Field is required")]
    public string Address {get; set;}

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    //Foregin Key Association
    public int UserId {get; set;}
    public User? Creator {get; set;}

    // users that RSVP'd
    public List<RSVP>?  UserRSVPs {get; set;}
}

public class DateMustBeInFutureAttribute : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        return DateTime.Compare(((DateTime)value), DateTime.Today) < 0 ? new ValidationResult("Date cannot be in the past") : ValidationResult.Success;
    }
}