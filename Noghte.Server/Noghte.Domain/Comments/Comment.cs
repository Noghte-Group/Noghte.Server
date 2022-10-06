using Noghte.Domain.Posts;
using Noghte.Domain.Users;

namespace Noghte.Domain.Comments;

public class Comment : Entity
{
    public long PostId { get; set; }
    public long UserId { get; set; }
    public string Body { get; set; }
    public DateTime CreatedAt { get; set; }
    public long LikeCount { get; set; }
    #region Relations

    public Post Post { get; set; }
    
    public User User { get; set; }
    #endregion
}