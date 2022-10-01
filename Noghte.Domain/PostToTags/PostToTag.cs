namespace Noghte.Domain.PostToTags;

public class PostToTag
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public long TagId { get; set; }
}