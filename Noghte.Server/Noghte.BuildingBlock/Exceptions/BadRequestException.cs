namespace Noghte.BuildingBlock.Exceptions;

using Noghte.BuildingBlock;
using System;

public class BadRequestException : AppException
{
    public BadRequestException()
        : base(ConsumerStatusCode.BadRequest, System.Net.HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message)
        : base(ConsumerStatusCode.BadRequest, message, System.Net.HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(object additionalData)
        : base(ConsumerStatusCode.BadRequest, null, System.Net.HttpStatusCode.BadRequest, additionalData)
    {
    }

    public BadRequestException(string message, object additionalData)
        : base(ConsumerStatusCode.BadRequest, message, System.Net.HttpStatusCode.BadRequest, additionalData)
    {
    }

    public BadRequestException(string message, Exception exception)
        : base(ConsumerStatusCode.BadRequest, message, exception, System.Net.HttpStatusCode.BadRequest)
    {
    }

    public BadRequestException(string message, Exception exception, object additionalData)
        : base(ConsumerStatusCode.BadRequest, message, System.Net.HttpStatusCode.BadRequest, exception, additionalData)
    {
    }
}