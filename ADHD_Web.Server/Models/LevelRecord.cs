namespace ADHD_Web.Server.Models;

public class LevelRecord
{
    public Guid Id { get; internal set; }
    public Guid PlayerId { get; set; }
    public int LevelNumber { get; set; }
    public string Difficulty { get; set; } = "normal";
    public int Score { get; set; }
    public int Time { get; set; }
    public DateTime CreatedAt { get; internal set; }
}