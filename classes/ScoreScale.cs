public class ScoreScale
{
    public int ScaleId { get; set; }
    public int PlayerId { get; set; }
    public int Scale1 { get; set; }
    public int Scale2 { get; set; }
    public int Scale3 { get; set; }
    public int? LastEventId { get; set; }
    public DateTime UpdatedAt { get; set; }

    public Player Player { get; set; }
}