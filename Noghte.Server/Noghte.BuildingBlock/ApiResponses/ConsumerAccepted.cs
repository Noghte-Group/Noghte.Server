using System.Net;
using Noghte.BuildingBlock.Exceptions;

namespace Noghte.BuildingBlock.ApiResponses;

public class ConsumerAccepted<TResult>
{
    public ConsumerAccepted()
    {
        HttpStatusCode = HttpStatusCode.OK;
    }

    public TResult? Result { get; set; }
    public string Message { get; }
    public HttpStatusCode HttpStatusCode { get; init; }
    public ConsumerStatusCode ConsumerStatusCode { get; set; }
}