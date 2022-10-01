using Noghte.Domain.Categories;
using Noghte.Domain.Posts;

namespace Noghte.Domain.Users;

public class User
{
    public long Id { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string PhoneNumber { get; set; }

    public string Email { get; set; }

    #region Realations

    public List<Category> Categories { get; set; }

    public List<Post> Posts { get; set; }

    #endregion
}