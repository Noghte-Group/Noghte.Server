using System.Net;
using Noghte.BuildingBlock.Exceptions;

namespace Noghte.BuildingBlock.ApiResponses;

public class ConsumerAccepted<TContract> 
    where TContract : class, IContract 
{
    public ConsumerAccepted()
    {
        StatusCode = ConsumerStatusCode.Success;
    }

    public TContract? Result { get; set; }
    public string Message { get; set; }
    public ConsumerStatusCode StatusCode { get; set; }
}