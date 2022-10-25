namespace Noghte.BuildingBlock.Exceptions;

using Noghte.BuildingBlock;
using System;

public class NotFoundException : AppException
{
    public NotFoundException()
        : base(ConsumerStatusCode.NotFound, System.Net.HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string message)
        : base(ConsumerStatusCode.NotFound, message, System.Net.HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(object additionalData)
        : base(ConsumerStatusCode.NotFound, null, System.Net.HttpStatusCode.NotFound, additionalData)
    {
    }

    public NotFoundException(string message, object additionalData)
        : base(ConsumerStatusCode.NotFound, message, System.Net.HttpStatusCode.NotFound, additionalData)
    {
    }

    public NotFoundException(string message, Exception exception)
        : base(ConsumerStatusCode.NotFound, message, exception, System.Net.HttpStatusCode.NotFound)
    {
    }

    public NotFoundException(string message, Exception exception, object additionalData)
        : base(ConsumerStatusCode.NotFound, message, System.Net.HttpStatusCode.NotFound, exception, additionalData)
    {
    }
}