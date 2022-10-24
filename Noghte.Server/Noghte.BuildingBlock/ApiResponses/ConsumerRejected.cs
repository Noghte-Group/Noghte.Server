using System.Net;
using Noghte.BuildingBlock.Exceptions;

namespace Noghte.BuildingBlock.ApiResponses;

public class ConsumerRejected
{
    public ConsumerStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}