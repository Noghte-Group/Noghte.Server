using Noghte.Domain.FavoriteCategories;
using Noghte.Domain.Posts;
using Noghte.Domain.Users;

namespace Noghte.Domain.Categories;

public class Category : Entity
{
    public long UserId { get; set; }

    public string ImageUrl { get; set; }

    public string Title { get; set; }

    #region Reltions
    public List<Post> Posts { get; set; }

    public User User { get; set; }
    public List<FavoriteCategory> FavoriteCategories { get; set; }
    #endregion
}