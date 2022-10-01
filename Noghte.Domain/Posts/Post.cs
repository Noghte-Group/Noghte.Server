namespace Noghte.Domain.Users;

public class Post
{
    public long  Id { get; set; }

    public long CategoryId { get; set; }

    public long UserId { get; set; }

    public string Body { get; set; }

    public string Description { get; set; }

    public string ImageUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public int ReadTime { get; set; }

    public int LikeCount { get; set; }

    #region Realations

    public List<PostToTag> PostToTags { get; set; }

    public List<Comment> Comments { get; set; }
    #endregion

}