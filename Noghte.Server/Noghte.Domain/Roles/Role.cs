using Noghte.Domain.Users;

namespace Noghte.Domain.Roles;

public class Role : Entity
{
    public string Title { get; set; }
    public string Description { get; set; }

    #region Relations

    public List<User> Users { get; set; }

    #endregion
}