using Noghte.Domain.Bookmarks;
using Noghte.Domain.Categories;
using Noghte.Domain.Comments;
using Noghte.Domain.FavoriteCategories;
using Noghte.Domain.Followings;
using Noghte.Domain.LikedPosts;
using Noghte.Domain.Posts;
using Noghte.Domain.Roles;

namespace Noghte.Domain.Users;

public class User : Entity

{
    public long RoleId { get; set; }
    public string UserName { get; set; }
    public string PasswordHash { get; set; }
    public string FirstName { get; set; }
    public string PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string? Bio { get; set; }
    public DateTime LastLoginDate { get; set; }

    #region Relations

    public List<Category> Categories { get; set; }

    public List<Post> Posts { get; set; }

    public List<Following> Followings { get; set; }

    public List<Comment> Comments { get; set; }
    
    public List<Bookmark> Bookmarks { get; set; }
    public Role Role { get; set; }
    public List<FavoriteCategory> FavoriteCategories { get; set; }
    public List<LikedPost> LikedPosts { get; set; }
    #endregion
}