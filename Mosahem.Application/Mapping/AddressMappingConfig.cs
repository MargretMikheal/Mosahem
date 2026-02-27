using Mapster;
using mosahem.Domain.Entities.Location;
using Mosahem.Application.Features.Addresses.Commands.Organization.AddOrganizationAddress;
using Mosahem.Application.Features.Addresses.Commands.Organization.EditOrganizationAddress;

namespace Mosahem.Application.Mapping
{
    public class AddressMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            #region Command Mapping
            config.NewConfig<AddOrganizationAddressCommand, Address>()
                .Map(dest => dest.Id, src => Guid.NewGuid())
                .Map(dest => dest.CityId, src => src.CityID)
                .Map(dest => dest.Description, src => src.Description)
                .Map(dest => dest.OrganizationId, src => src.OrganizationId)
                .Map(dest => dest.CreatedAt, src => DateTime.UtcNow)
                .IgnoreNonMapped(true);

            config.NewConfig<EditOrganizationAddressCommand, Address>()
                .Map(dest => dest.CityId, src => src.CityId)
                .Map(dest => dest.Description, src => src.Description)
                .IgnoreNullValues(true);
            #endregion
        }
    }
}
