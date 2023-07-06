using FluentValidation.Results;
using scada_back.Alarm;
using scada_back.Exception;
using scada_back.Tag.Model.Abstraction;
using scada_back.Validation.Validators;

namespace scada_back.Validation;

public class ValidationService : IValidationService
{
    private readonly EmptyStringValidator _emptyStringValidator;
    private readonly TagValidator _tagValidator;
    private readonly AlarmValidator _alarmValidator;

    public ValidationService()
    {
        _emptyStringValidator = new EmptyStringValidator();
        _tagValidator = new TagValidator();
        _alarmValidator = new AlarmValidator();
    }
    
    public void ValidateEmptyString(string parameterName, string parameter)
    {
        _emptyStringValidator.ParameterName = parameterName;
        ValidationResult result = _emptyStringValidator.Validate(parameter);
        if (!result.IsValid)
        {
            throw new InvalidParameterException(result.Errors.First().ToString());
        }
    }

    public void ValidateTag(TagDto tag)
    {
        ValidationResult result = _tagValidator.Validate(tag);
        if (!result.IsValid)
        {
            throw new InvalidParameterException(result.Errors.First().ToString());
        }
    }

    public void ValidateAlarm(AlarmDto alarm)
    {
        ValidationResult result = _alarmValidator.Validate(alarm);
        if (!result.IsValid)
        {
            throw new InvalidParameterException(result.Errors.First().ToString());
        }
    }
}