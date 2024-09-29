public class ChatMessage
{
    public int MessageId { get; set; }
    public int PlayerId { get; set; }
    public string Message { get; set; }
    public DateTime CreatedAt { get; set; }

    public Player Player { get; set; }
}