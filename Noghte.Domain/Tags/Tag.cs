using Noghte.Domain.PostToTags;

namespace Noghte.Domain.Tags;

public class Tag
{
    public long Id { get; set; }

    public string Title { get; set; }

    #region Relations

    public List<PostToTag> PostToTags { get; set; }

    #endregion
}