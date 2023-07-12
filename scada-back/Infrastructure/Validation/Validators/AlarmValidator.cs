using FluentValidation;
using scada_back.Infrastructure.Feature.Alarm;

namespace scada_back.Infrastructure.Validation.Validators;

public class AlarmValidator : AbstractValidator<AlarmDto>
{
    public AlarmValidator()
    {
        RuleFor(alarm => alarm.TagName)
            .NotEmpty().WithMessage("TagName is required.")
            .Length(1, 20).WithMessage("TagName should have a length between 1 and 20.");
       
        RuleFor(alarm => alarm.Type)
            .NotEmpty().WithMessage("Type is required.")
            .Must(type => (type.ToUpper() == "ABOVE" || type.ToUpper() == "BELLOW"))
            .WithMessage("Type should be either 'ABOVE' or 'BELLOW' for alarm.");
        
        RuleFor(alarm => alarm.AlarmPriority)
            .Must(priority => ((int)priority <= 2 && (int)priority >= 0))
            .WithMessage("Alarm priority should be between 0 and 2.");
        
        RuleFor(alarm => alarm.AlarmName)
            .NotEmpty().WithMessage("AlarmName is required.")
            .Length(1, 20).WithMessage("AlarmName should have a length between 1 and 20.");

    }
}