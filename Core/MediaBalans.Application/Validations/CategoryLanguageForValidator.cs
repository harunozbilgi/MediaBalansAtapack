using FluentValidation;
using MediaBalans.Domain.Entities.Languages;


namespace MediaBalans.Application.Validations
{
    public class CategoryLanguageForValidator : AbstractValidator<CategoryLanguage>
    {
        public CategoryLanguageForValidator()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Required field please fill.");
        }
    }
}
