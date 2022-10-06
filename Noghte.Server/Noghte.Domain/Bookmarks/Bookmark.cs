using Noghte.BuildingBlock.Common;
using Noghte.Domain.Posts;
using Noghte.Domain.Users;

namespace Noghte.Domain.Bookmarks;

public class Bookmark : Entity
{

    public long UserId { get; set; }
    public long PostId { get; set; }

    #region Relations

    public User User { get; set; }
    
    public Post Post { get; set; }
    #endregion
}