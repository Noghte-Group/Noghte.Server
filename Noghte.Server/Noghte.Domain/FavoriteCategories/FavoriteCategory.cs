using Noghte.Domain.Categories;

using Noghte.Domain.Users;

namespace Noghte.Domain.FavoriteCategories;

public class FavoriteCategory : Entity
{
    public long UserId { get; set; }
    public long CategoryId { get; set; }

    #region Relations
    
    public Category Category { get; set; }
    public User User { get; set; }

    #endregion
}
