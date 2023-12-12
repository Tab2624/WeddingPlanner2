#pragma warning disable CS8618
using System.ComponentModel.DataAnnotations;
// Add this using statement to access NotMapped
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc;
namespace WeddingPlanner2.Models;
public class RSVP
{
    [Key]
    public int RSVPId { get; set; }

    public int UserId { get; set; }
    public int WeddingId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime UpdatedAt { get; set; } = DateTime.Now;

    public User? RSVPUser { get; set; }
    public Wedding? RSVPWedding { get; set; }
}