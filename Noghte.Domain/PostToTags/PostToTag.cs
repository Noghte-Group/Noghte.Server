namespace Noghte.Domain.Users;

public class PostToTag
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public long TagId { get; set; }
}