namespace ADHD_Web.Server.Models;

public class PlayerLevel
{
    public int PlayerId { get; set; }
    public required Players Player { get; set; }
    public int LevelId { get; set; }
    public required Levels Level { get; set; }
    public bool IsCompleted { get; set; }
    public long TimeSpent { get; set; }
}
