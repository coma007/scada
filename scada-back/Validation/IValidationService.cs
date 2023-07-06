using FluentValidation.Results;
using scada_back.Alarm;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Validation;

public interface IValidationService
{
    public void ValidateEmptyString(string parameterName, string parameter);
    public void ValidateTag(TagDto tag);
    public void ValidateAlarm(AlarmDto alarm);
}