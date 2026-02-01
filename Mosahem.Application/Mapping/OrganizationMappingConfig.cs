using Mapster;
using mosahem.Application.Features.Authentication.Commands.CompleteOrganizationRegistration;
using mosahem.Application.Features.Authentication.Commands.ValidateOrganizationLocations;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Identity;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Profiles;
using mosahem.Domain.Enums;

namespace mosahem.Application.Mapping
{
    public class OrganizationMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CompleteOrganizationRegistrationCommand, MosahmUser>()
                .Map(dest => dest.FullName, src => src.OrganizationName)
                .Map(dest => dest.UserName, src => src.Email)
                .Map(dest => dest.Email, src => src.Email)
                .Map(dest => dest.PhoneNumber, src => src.PhoneNumber)
                .Map(dest => dest.Role, src => UserRole.Organization)
                .Map(dest => dest.AuthProvider, src => AuthProvider.Local)
                .Map(dest => dest.IsDeleted, src => false)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow);

            config.NewConfig<CompleteOrganizationRegistrationCommand, Organization>()
                .Map(dest => dest.Description, src => src.Description ?? string.Empty)
                .Map(dest => dest.LicenseKey, src => src.LicenseUrl)
                .Map(dest => dest.VerificationStatus, src => VerficationStatus.Pending);

            config.NewConfig<OrganizationAddressDto, Address>()
                .Map(dest => dest.CityId, src => src.CityId)
                .Map(dest => dest.Description, src => src.Details)
                .Ignore(dest => dest.Id)
                .Ignore(dest => dest.OrganizationId);

            config.NewConfig<Guid, OrganizationField>()
                .Map(dest => dest.FieldId, src => src)
                .Ignore(dest => dest.OrganizationId);
        }
    }
}