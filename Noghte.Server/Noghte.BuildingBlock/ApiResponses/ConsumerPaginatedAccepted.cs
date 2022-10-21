namespace Noghte.BuildingBlock.ApiResponses;

public class ConsumerPaginatedAccepted<TContract>
    where TContract : class, IContract
{
    public ConsumerPaginatedAccepted()
    {
        StatusCode = ConsumerStatusCode.Success;
    }
    
    public List<TContract> Data { get; set; }

    public Pagination Pagination { get; set; }

    public ConsumerStatusCode StatusCode { get; set; }

    public string Message { get; set; }
}