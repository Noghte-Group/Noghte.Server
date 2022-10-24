using Noghte.Domain.Bookmarks;
using Noghte.Domain.Categories;
using Noghte.Domain.Comments;
using Noghte.Domain.LikedPosts;
using Noghte.Domain.PostToTags;
using Noghte.Domain.Users;

namespace Noghte.Domain.Posts;

public class Post : Entity
{
    public long CategoryId { get; set; }
    public long UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Body { get; set; }
    public string ImageUrl { get; set; }
    public DateTime CreatedAt { get; set; }
    public int ReadingTime { get; set; }
    public int LikeCount { get; set; }
    #region Relations

    public List<PostToTag> PostToTags { get; set; }
    public List<Comment> Comments { get; set; }
    
    public User User { get; set; }
    public Category Category { get; set; }

    public List<Bookmark> Bookmarks { get; set; }
    public List<LikedPost> LikedPosts { get; set; }

    #endregion
}