namespace Mosahem.Application.Features.Opportunities.Commands.EditOpportunityInfo
{
    public class EditOpportunityInfoRequest
    {
        public string? Title { get; set; }
        public string? Description { get; set; }
        public int? Vacancies { get; set; }
        public List<EditOpportunityInfoAddressDto>? Addresses { get; set; }
    }
}
