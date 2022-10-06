using Noghte.Domain.Posts;
using Noghte.Domain.Tags;

namespace Noghte.Domain.PostToTags;

public class PostToTag : Entity
{
    public long Id { get; set; }

    public long PostId { get; set; }

    public long TagId { get; set; }

    #region Relations

    public Post Post { get; set; }
    public Tag Tag { get; set; }

    #endregion
}