using FluentValidation;
using scada_back.Tag.Model;
using scada_back.Tag.Model.Abstraction;

namespace scada_back.Validation.Validators;

public class TagValidator : AbstractValidator<TagDto>
{
    public TagValidator()
    {
        RuleFor(tag => tag.TagName)
            .NotEmpty().WithMessage("TagName is required.")
            .Length(1, 20).WithMessage("TagName should have a length between 1 and 20.");

        RuleFor(tag => tag.Description)
            .Length(0, 200).WithMessage("Description must have less than 200 characters.");
       
        RuleFor(tag => tag.TagType)
            .NotEmpty().WithMessage("TagType is required");

        RuleFor(tag => tag)
            .Must(tag => tag is IInputTagDto inputTag &&
                         (inputTag.Driver.Trim().ToUpper() == "SIMULATION" || inputTag.Driver.Trim().ToUpper() == "REALTIME"))
            .WithMessage("Driver should be either 'SIMULATION' or 'REALTIME' for input tags.")
            .Must(tag => tag is IInputTagDto inputTag && inputTag.ScanTime != 0)
            .WithMessage("Scan time must be positive number of seconds.");
        
        RuleFor(tag => tag)
            .Must(tag => tag is IAnalogTagDto analogTag && 
                         (analogTag.LowLimit < analogTag.HighLimit))
            .WithMessage("LowLimit should be lower than HighLimit.");
         
        RuleFor(tag => tag)
            .Must(tag => tag is DigitalOutputTagDto digitalOutputTag && 
                         (digitalOutputTag.InitialValue == 0 || Math.Abs(digitalOutputTag.InitialValue - 1.0) < 0))
            .WithMessage("InitialValue of digital output tag should be 0 or 1.");
    }
}