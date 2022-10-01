namespace Noghte.Domain.Users;

public class Comment
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public string Body { get; set; }

    public DateTime CreatedAt { get; set; }
}