using FluentValidation;
using MediaBalans.Domain.Entities.Languages;

namespace MediaBalans.Application.Validations
{
    public class PortfolioLanguageForValidator : AbstractValidator<PortfolioLanguage>
    {
        public PortfolioLanguageForValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Required field please fill.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Required field please fill.");
        }
    }
}
