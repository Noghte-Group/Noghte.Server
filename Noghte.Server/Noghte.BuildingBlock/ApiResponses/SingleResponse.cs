namespace Noghte.BuildingBlock.ApiResponses;

public class SingleResponse<T>
{
    public T Data { get; set; }
}

public class SingleResponse
{
    public long Data { get; set; }
}