namespace ADHD_Web.Server.Models;

public class LevelRecord
{
    public Guid Id { get; set; }
    public Guid PlayerId { get; set; }
    public int LevelNumber { get; set; }
    public string Difficulty { get; set; } = "Normal";
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
    public DateTime CreatedAt { get; set; }

    public Player Player { get; set; }
}