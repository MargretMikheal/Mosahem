using FluentValidation;
using Microsoft.Extensions.Localization;
using mosahem.Application.Resources;

namespace Mosahem.Application.Features.Organizations.Queries.GetOrganizationVerificationComment
{
    public class GetOrganizationVerificationCommentQueryValidator : AbstractValidator<GetOrganizationVerificationCommentQuery>
    {
        public GetOrganizationVerificationCommentQueryValidator(IStringLocalizer<SharedResources> localizer)
        {
            RuleFor(x => x.OrganizationId)
                .NotEmpty().WithMessage(localizer[SharedResourcesKeys.Validation.Required]);
        }
    }
}
