public class Response
{
    public int ResponseId { get; set; }
    public int PlayerId { get; set; }
    public int EventId { get; set; }
    public int ResponseOption { get; set; }
    public DateTime CreatedAt { get; set; }

    public Player Player { get; set; }
    public Event Event { get; set; }
}