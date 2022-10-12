using Noghte.Domain.Posts;
using Noghte.Domain.Users;

namespace Noghte.Domain.LikedPosts;

public class LikedPost : Entity
{
    public long UserId { get; set; }
    public long PostId { get; set; }

    #region Relations

    public User Users { get; set; }
    public Post Post { get; set; }

    #endregion   
}
