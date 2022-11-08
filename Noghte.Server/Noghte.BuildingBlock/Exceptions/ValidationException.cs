using FluentValidation.Results;
using Noghte.BuildingBlock.ApiResponses;

namespace Noghte.BuildingBlock.Exceptions;

public class ValidationException : AppException
{
    public List<ValidationJsonMessage> Errors { get; set; }

    public ValidationException()
    {
        Errors = new List<ValidationJsonMessage>();
    }

    public ValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        var failureGroups = failures.GroupBy(e => e.PropertyName, e => e.ErrorMessage);

        foreach (var failureGroup in failureGroups)
        {
            var propertyName = failureGroup.Key;
            var propertyFailures = failureGroup.ToArray();

            var Error = new ValidationJsonMessage{Property = propertyName, Errors = propertyFailures };

            Errors.Add(Error);
        }
    }
}

