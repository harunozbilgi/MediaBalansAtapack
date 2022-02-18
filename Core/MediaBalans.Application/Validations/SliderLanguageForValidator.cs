using FluentValidation;
using MediaBalans.Domain.Entities.Languages;


namespace MediaBalans.Application.Validations
{
    public class SliderLanguageForValidator : AbstractValidator<SliderLanguage>
    {
        public SliderLanguageForValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Required field please fill.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Required field please fill.");
        }
    }
}
