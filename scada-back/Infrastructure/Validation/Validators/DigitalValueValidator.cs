using FluentValidation;

namespace scada_back.Infrastructure.Validation.Validators;

public class DigitalValueValidator: AbstractValidator<double>
{
    public DigitalValueValidator()
    {
        RuleFor(value => value)
            .NotEmpty()
            .Must(value => value is 0 or 1)
            .WithMessage($"Value of digital tag should be 0 or 1.");
    }
}