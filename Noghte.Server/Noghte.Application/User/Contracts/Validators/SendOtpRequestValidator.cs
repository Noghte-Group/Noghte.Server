using FluentValidation;
using Noghte.BuildingBlock.ConsumerMessages;

namespace Noghte.Application.User.Contracts.Validators;

public class SendOtpRequestValidator : AbstractValidator<SendRegisterOtpRequest>
{
    public SendOtpRequestValidator()
    {
        RuleFor(r => r.PhoneNumber)
            .NotEmpty()
            .WithMessage(FluentValidationMessage.CANNOT_BE_EMPTY("Phone Number"));
    }
}
