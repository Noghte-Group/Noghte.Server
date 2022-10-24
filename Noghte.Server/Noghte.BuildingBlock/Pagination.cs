namespace Noghte.BuildingBlock.Exceptions;

public class Pagination
{
    public int PageIndex { get; set; }

    public int RowsPerPage { get; set; }

    public int TotalCount { get; set; }

    public bool Descending { get; set; }

    public string OrderBy { get; set; }
}