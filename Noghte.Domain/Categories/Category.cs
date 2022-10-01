using Noghte.Domain.Posts;
using Noghte.Domain.Users;

namespace Noghte.Domain.Categories;

public class Category
{
    public long Id { get; set; }

    public long UserId { get; set; }

    public string ImageUrl { get; set; }

    public string Title { get; set; }

    #region Realtions

    public List<Post> Posts { get; set; }

    #endregion
}