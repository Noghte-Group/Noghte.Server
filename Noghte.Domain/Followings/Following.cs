namespace Noghte.Domain;

public class Following
{
    public long Id { get; set; }

    public long FollowerId { get; set; }

    public long FollowedUserId { get; set; }
}