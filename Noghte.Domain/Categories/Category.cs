namespace Noghte.Domain.Users;

public class Category
{
    public long Id { get; set; }

    public long UserId { get; set; }
    
    public string ImageUrl { get; set; }

    public string Title { get; set; }

    public List<Post> Posts { get; set; }

}