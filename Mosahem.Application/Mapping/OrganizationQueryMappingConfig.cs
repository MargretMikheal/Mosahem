using Mapster;
using mosahem.Domain.Entities;
using mosahem.Domain.Entities.Location;
using mosahem.Domain.Entities.Profiles;
using Mosahem.Application.Features.Organization.Queries.GetAllOrganizations;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationData.Mosahem.Application.Features.Organization.Queries.GetOrganizationData;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationFields;
using Mosahem.Application.Features.Organization.Queries.GetOrganizationLocations;

namespace Mosahem.Application.Mapping
{
    public class OrganizationQueryMappingConfig : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<OrganizationField, GetOrganizationFieldsResponse>()
                .Map(dest => dest.Id, src => src.FieldId)
                .Map(dest => dest.Name, src => src.Field.Localize(src.Field.NameAr, src.Field.NameEn));

            config.NewConfig<OrganizationField, GetOrganizationDataFieldResponse>()
                .Map(dest => dest.Id, src => src.FieldId)
                .Map(dest => dest.Name, src => src.Field.Localize(src.Field.NameAr, src.Field.NameEn));

            config.NewConfig<Address, GetOrganizationLocationsResponse>()
                .Map(dest => dest.CityName, src => src.City.Localize(src.City.NameAr, src.City.NameEn))
                .Map(dest => dest.GovernorateId, src => src.City.GovernorateId)
                .Map(dest => dest.GovernorateName,
                    src => src.City.Governorate.Localize(src.City.Governorate.NameAr, src.City.Governorate.NameEn));

            config.NewConfig<Address, GetOrganizationDataAddressResponse>()
                .Map(dest => dest.CityName, src => src.City.Localize(src.City.NameAr, src.City.NameEn))
                .Map(dest => dest.GovernorateId, src => src.City.GovernorateId)
                .Map(dest => dest.GovernorateName,
                    src => src.City.Governorate.Localize(src.City.Governorate.NameAr, src.City.Governorate.NameEn));


            config.NewConfig<Organization, GetAllOrganizationsResponse>()
                .Map(dest => dest.OrganizationId, src => src.Id)
                .Map(dest => dest.OrganizationName, src => src.User != null ? src.User.FullName : string.Empty)
                .Map(dest => dest.OrganizationDescription, src => src.Description)
                .Map(dest => dest.OrganizationLogo, src => src.LogoKey);

            config.NewConfig<Organization, GetOrganizationDataResponse>()
                .Map(dest => dest.OrganizationId, src => src.Id)
                .Map(dest => dest.OrganizationName, src => src.User != null ? src.User.FullName : string.Empty)
                .Map(dest => dest.OrganizationDescription, src => src.Description)
                .Map(dest => dest.OrganizationLogo, src => src.LogoKey)
                .Map(dest => dest.VerificationStatus, src => src.VerificationStatus.ToString())
                .Map(dest => dest.VerificationComment, src => src.VerificationComment)
                .Map(dest => dest.Fields,
                    src => (src.OrganizationFields ?? new List<OrganizationField>()).ToList())
                .Map(dest => dest.Locations,
                    src => (src.Address ?? new List<Address>()).ToList());
        }
    }
}

