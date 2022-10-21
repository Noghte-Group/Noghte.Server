using System.Net;

namespace Noghte.BuildingBlock.ApiResponses;

public class ConsumerRejected
{
    public ConsumerStatusCode StatusCode { get; set; }
    public string Reason { get; init; }
    public string Message { get; }
}