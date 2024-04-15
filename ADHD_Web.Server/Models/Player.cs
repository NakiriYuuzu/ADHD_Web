using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADHD_Web.Server.Models;

public class Player
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; internal set; }
    public string Name { get; set; }
    public int Age { get; set; }
    public string Gender { get; set; }
    public DateTime CreatedAt { get; internal set; }
}