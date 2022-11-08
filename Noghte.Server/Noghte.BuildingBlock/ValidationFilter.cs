using FluentValidation;
using MassTransit;

namespace Noghte.BuildingBlock;

public class ValidationFilter<TMessage> : IFilter<ConsumeContext<TMessage>>
    where TMessage : class
{
    private readonly IEnumerable<IValidator<TMessage>> _validators;

    public ValidationFilter(IEnumerable<IValidator<TMessage>> validators) => _validators = validators;

    public void Probe(ProbeContext context)
    {
    }

    public async Task Send(ConsumeContext<TMessage> context, IPipe<ConsumeContext<TMessage>> next)
    {
        var request = context.Message;
        var cancellation = context.CancellationToken;

        if (_validators.Any())
        {
            var validationResults = await Task.WhenAll(_validators.Select(v => v.ValidateAsync(request, cancellation)));
            var errors = validationResults.SelectMany(r => r.Errors).Where(f => f != null).ToList();

            if (errors.Count != 0)
            {
                throw new Exceptions.ValidationException(errors);
            }
        }

        await next.Send(context);
    }
}
