namespace Noghte.BuildingBlock.Exceptions;

using System;

public class LogicException : AppException
{
    public LogicException()
        : base(ConsumerStatusCode.LogicError)
    {
    }

    public LogicException(string message)
        : base(ConsumerStatusCode.LogicError, message)
    {
    }

    public LogicException(object additionalData)
        : base(ConsumerStatusCode.LogicError, additionalData)
    {
    }

    public LogicException(string message, object additionalData)
        : base(ConsumerStatusCode.LogicError, message, additionalData)
    {
    }

    public LogicException(string message, Exception exception)
        : base(ConsumerStatusCode.LogicError, message, exception)
    {
    }

    public LogicException(string message, Exception exception, object additionalData)
        : base(ConsumerStatusCode.LogicError, message, exception, additionalData)
    {
    }
}