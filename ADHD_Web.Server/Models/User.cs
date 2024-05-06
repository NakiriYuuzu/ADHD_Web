using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADHD_Web.Server.Models;

public class User
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; internal set; }
    public required string Username { get; set; }
    public required string Password { get; set; }
    public string? Token { get; internal set; }
    public DateTime CreatedAt { get; internal set; }
}