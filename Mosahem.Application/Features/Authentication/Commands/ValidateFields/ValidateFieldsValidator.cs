using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Interfaces.Repositories;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Authentication.Commands.ValidateFields
{
    public class ValidateFieldsValidator : AbstractValidator<ValidateFieldsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResources> _localizer;

        public ValidateFieldsValidator(IUnitOfWork unitOfWork, IStringLocalizer<SharedResources> localizer)
        {
            _unitOfWork = unitOfWork;
            _localizer = localizer;

            RuleFor(x => x.FieldIds)
                .NotNull().NotEmpty().WithMessage(_localizer[SharedResourcesKeys.Validation.Required])
                .Must(ids => ids.Distinct().Count() == ids.Count).WithMessage(SharedResourcesKeys.Validation.DuplicateEntry);

            RuleForEach(x => x.FieldIds)
                .MustAsync(async (id, ct) => await _unitOfWork.Fields.GetByIdAsync(id) != null)
                .WithMessage(_localizer[SharedResourcesKeys.Validation.NotFound]);
        }
    }
}
