using FluentValidation;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Validations
{
    public class NewsLanguageForValidator : AbstractValidator<NewsLanguage>
    {
        public NewsLanguageForValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Required field please fill.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Required field please fill.");
        }
    }
}
