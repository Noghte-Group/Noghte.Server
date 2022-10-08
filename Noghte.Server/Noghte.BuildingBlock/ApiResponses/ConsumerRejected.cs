using System.Net;
using Noghte.BuildingBlock.Exceptions;

namespace Noghte.BuildingBlock.ApiResponses;

public class ConsumerRejected
{
    public ConsumerStatusCode ConsumerStatusCode { get; set; }
    public HttpStatusCode HttpStatusCode { get; init; }
    public Dictionary<string, string[]> Reasons { get; init; }
    public string Message { get; }
}