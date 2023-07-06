using FluentValidation;

namespace scada_back.Validation.Validators;

public class EmptyStringValidator: AbstractValidator<string>
{
    public string ParameterName { get; set; } = string.Empty;
    public EmptyStringValidator()
    {
        RuleFor(str => str)
            .NotEmpty()
            .WithMessage($"Parameter '{ParameterName}' should not be empty.");
    }
}