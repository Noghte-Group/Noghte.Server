using Noghte.Domain.Users;

namespace Noghte.Domain.Followings;

public class Following : Entity
{
    public long FollowerId { get; set; }

    public long FollowedUserId { get; set; }

    #region Relation

    public User User { get; set; }
    
    #endregion    
}