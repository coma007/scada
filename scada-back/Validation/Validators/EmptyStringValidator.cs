using FluentValidation;

namespace scada_back.Validation.Validators;

public class EmptyStringValidator: AbstractValidator<string>
{
    public string ParameterName { get; set; }
    public EmptyStringValidator()
    {
        RuleFor(str => str.Trim())
            .NotEmpty()
            .WithMessage($"Parameter '{ParameterName}' should not be empty.");

    }
}