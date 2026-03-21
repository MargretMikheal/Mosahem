using MediatR;
using mosahem.Application.Common;

namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityInfo
{
    public class EditOpportunityInfoCommand : IRequest<Response<string>>
    {
        public Guid OpportunityId { get; set; }
        public Guid OrganizationId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Vacancies { get; set; }
        public List<EditOpportunityInfoAddressDto>? Addresses { get; set; }
    }
    public class EditOpportunityInfoAddressDto
    {
        public Guid? AddressId { get; set; }
        public Guid GovernorateId { get; set; }
        public Guid CityId { get; set; }
        public string? Description { get; set; }
    }
}
