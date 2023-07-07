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
    private readonly SignalTypeValidator _signalTypeValidator;
    private readonly DigitalValueValidator _digitalValueValidator;

    public ValidationService()
    {
        _emptyStringValidator = new EmptyStringValidator();
        _signalTypeValidator = new SignalTypeValidator();
        _digitalValueValidator = new DigitalValueValidator();
        _tagValidator = new TagValidator();
        _alarmValidator = new AlarmValidator();
    }
    
    public void ValidateEmptyString(string parameterName, string parameter)
    {
        _emptyStringValidator.ParameterName = parameterName;
        ValidationResult result = _emptyStringValidator.Validate(parameter);
        ThrowException(result);
    }

    public void ValidateSignalType(string signalType)
    {
        _signalTypeValidator.SignalType = signalType;
        ValidationResult result = _signalTypeValidator.Validate(signalType);
        ThrowException(result);
    }

    public void ValidateDigitalValue(double value)
    {
        ValidationResult result = _digitalValueValidator.Validate(value);
        ThrowException(result);
    }

    public void ValidateTag(TagDto tag)
    {
        ValidationResult result = _tagValidator.Validate(tag);
        ThrowException(result);
    }

    public void ValidateAlarm(AlarmDto alarm)
    {
        ValidationResult result = _alarmValidator.Validate(alarm);
        ThrowException(result);
        
    }

    private static void ThrowException(ValidationResult result)
    {
        if (!result.IsValid)
        {
            throw new InvalidParameterException(result.Errors.First().ToString());
        }
    }
}