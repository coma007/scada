using FluentValidation;

namespace scada_back.Validation.Validators;

public class SignalTypeValidator: AbstractValidator<string>
{
    public string SignalType { get; set; } = string.Empty;

    public SignalTypeValidator()
    {
        RuleFor(signalType => signalType)
            .Must(signalType => signalType != "analog" && signalType != "digital")
            .WithMessage($"Invalid signal type: {SignalType}.");
    }
}