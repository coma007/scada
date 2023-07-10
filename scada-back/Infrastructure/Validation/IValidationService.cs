using scada_back.Infrastructure.Feature.Alarm;
using scada_back.Infrastructure.Feature.Tag.Model.Abstraction;

namespace scada_back.Infrastructure.Validation;

public interface IValidationService
{
    public void ValidateEmptyString(string parameterName, string parameter);
    public void ValidateSignalType(string signalType);
    void ValidateDigitalValue(double value);
    public void ValidateTag(TagDto tag);
    public void ValidateAlarm(AlarmDto alarm);
}