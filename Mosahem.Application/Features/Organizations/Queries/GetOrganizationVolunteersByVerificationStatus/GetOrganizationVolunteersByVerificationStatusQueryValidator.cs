using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;
using mosahem.Domain.Enums;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVolunteersByVerificationStatus
{
    public class GetOrganizationVolunteersByVerificationStatusQueryValidator : AbstractValidator<GetOrganizationVolunteersByVerificationStatusQuery>
    {
        public GetOrganizationVolunteersByVerificationStatusQueryValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);

            RuleFor(x => x.VerificationStatus)
                .IsEnumName(typeof(ApplicantStatus), caseSensitive: false)
                .WithMessage(localizer[SharedResourcesKeys.Validation.Invalid]);
        }
    }
}
