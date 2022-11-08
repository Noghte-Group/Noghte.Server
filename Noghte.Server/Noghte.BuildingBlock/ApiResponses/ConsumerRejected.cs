namespace Noghte.BuildingBlock.ApiResponses;

public class ConsumerRejected
{
    public ConsumerStatusCode StatusCode { get; set; }
    public string Message { get; set; }
}

public class ConsumerValidationRejected
{
    public ConsumerStatusCode StatusCode { get; set; }
    public List<ValidationJsonMessage> Message { get; set; }
}

public class ValidationJsonMessage
{
    public string Property { get; set; }
    public string[] Errors { get; set; }
}