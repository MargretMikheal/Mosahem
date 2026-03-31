using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationAboutUs
{
    public class EditOrganizationAboutUsCommand : IRequest<Response<string>>
    {
        public EditOrganizationAboutUsCommand(Guid organizationId, string? aboutUs)
        {
            OrganizationId = organizationId;
            AboutUs = aboutUs;
        }

        public Guid OrganizationId { get; set; }
        public string? AboutUs { get; set; }
    }
}
