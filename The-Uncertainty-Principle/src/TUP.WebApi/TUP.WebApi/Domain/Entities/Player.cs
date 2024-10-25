namespace TUP.WebApi.Domain.Entities;

public class Player
{
    public int PlayerId { get; set; }
    public string UniquePlayerName { get; set; }
    public DateTime CreatedAt { get; set; }
}