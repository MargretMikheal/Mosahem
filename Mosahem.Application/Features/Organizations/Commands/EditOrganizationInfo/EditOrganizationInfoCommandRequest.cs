namespace Mosahem.Application.Features.Organizations.Commands.EditOrganizationInfo
{
    public class EditOrganizationInfoCommandRequest
    {
        public string? OrganizationName { get; set; }
        public string? OrganizationDescription { get; set; }
        public string? OrganizationPhoneNumber { get; set; }
    }
}
