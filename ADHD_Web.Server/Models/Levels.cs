using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADHD_Web.Server.Models;

public class Levels
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int LevelId { get; set; }
    public string? LevelName { get; set; }
    public required string Difficulty { get; set; }
    // public ICollection<PlayerLevel> PlayerLevels { get; set; }
}